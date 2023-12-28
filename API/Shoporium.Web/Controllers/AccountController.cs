using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shoporium.Business.Logins;
using Shoporium.Entities.DTO.Account;
using Shoporium.Web.Extensions;
using Shoporium.Web.Models.Account;
using System.Security.Claims;

namespace Shoporium.Web.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILoginFacade _loginFacade;

        public AccountController(IMapper mapper, ILoginFacade loginFacade)
        {
            _mapper = mapper;
            _loginFacade = loginFacade;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public ActionResult Register([FromBody] RegisterModel request)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            _loginFacade.Register(_mapper.Map<RegisterDTO>(request));

            var tokens = _loginFacade.Authenticate(_mapper.Map<LoginDTO>(request), GetIpAddress(), true);
            return Ok(new LoginResult(tokens.accessToken, tokens.refreshToken));
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginModel request)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var tokens = _loginFacade.Authenticate(_mapper.Map<LoginDTO>(request), GetIpAddress());
            return Ok(new LoginResult(tokens.accessToken, tokens.refreshToken));
        }

        [HttpPost("refresh-token")]
        public ActionResult RefreshToken([FromBody] RefreshTokenRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.RefreshToken)) return Unauthorized();

            var refreshTokenDto = _loginFacade.GetRefreshToken(request.RefreshToken, GetIpAddress());
            if (refreshTokenDto == null) return Unauthorized();

            var (accessToken, refreshToken) = _loginFacade.RefreshTokens(refreshTokenDto);
            return Ok(new LoginResult(accessToken, refreshToken));
        }

        [HttpGet("user")]
        public ActionResult GetCurrentUser()
        {
            var res = new LoginResult
            {
                Id = User.GetId(),
                FirstName = User.FindFirstValue("FirstName"),
                LastName = User.FindFirstValue("LastName"),
                Role = User.FindFirstValue(ClaimTypes.Role),
                Email = User.FindFirstValue(ClaimTypes.Email)
            };

            return Ok(res);
        }

        private string GetIpAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"]!;

            return HttpContext.Connection.RemoteIpAddress != null ?
                HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString() : string.Empty;
        }
    }
}
