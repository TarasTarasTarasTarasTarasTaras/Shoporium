using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Shoporium.Business.Accounts;
using Shoporium.Business.Auth;
using Shoporium.Business.Helpers;
using Shoporium.Business.Logins;
using Shoporium.Data._EntityFramework;
using Shoporium.Data.Accounts;
using Shoporium.Data.Logins;
using Shoporium.Data.RefreshTokens;
using Shoporium.Entities.Options;
using Shoporium.Web.Helpers;
using Shoporium.Web.Mapper;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ShoporiumContext>(options =>
    options.UseSqlServer(
        builder.Configuration["Data:ShoporiumDb:ConnectionString"],
        sqlOptions =>
        {
            sqlOptions.EnableRetryOnFailure();
        }));

// Auto Mapper Configurations
var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new BusinessMapper());
    mc.AddProfile(new WebMapper());
});

var mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<ILoginRepository, LoginRepository>();
builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

builder.Services.AddScoped<IAccountFacade, AccountFacade>();
builder.Services.AddScoped<ILoginFacade, LoginFacade>();
builder.Services.AddScoped<IAuthManagerFactory, AuthManagerFactory>();

#region Jwt authentication configuration

var jwtTokenOptions = builder.Configuration.GetSection(JwtTokenOptions.JwtToken).Get<JwtTokenOptions>();
builder.Services.AddSingleton(jwtTokenOptions!);

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    //x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = jwtTokenOptions.Issuer,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtTokenOptions.Secret)),
        ValidAudience = jwtTokenOptions.Audience,
        ValidateAudience = true,
        ValidateLifetime = true,
    };
    x.Events = new JwtBearerEvents()
    {
        OnMessageReceived = context =>
        {
            var accessToken = context.Request.Query["access_token"];

            // If the request is for our hub...
            var path = context.HttpContext.Request.Path;
            if (!string.IsNullOrEmpty(accessToken) &&
                (path.StartsWithSegments("/notify")))
            {
                // Read the token out of the query string
                context.Token = accessToken;
            }

            return Task.CompletedTask;
        }
    };
});

builder.Services.AddHostedService<JwtRefreshTokenCache>();

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var serviceScope = app.Services.CreateScope())
{
    var dbContext = serviceScope.ServiceProvider.GetRequiredService<ShoporiumContext>();
    dbContext.Database.Migrate();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
