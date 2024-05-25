using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shoporium.Business.Logins;
using Shoporium.Business.Users;
using Shoporium.Entities.DTO.Users;
using Shoporium.Web.Extensions;
using Shoporium.Web.Models.User;
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
        private readonly IUserFacade _userFacade;

        public AccountController(
            IMapper mapper,
            IUserFacade userFacade,
            ILoginFacade loginFacade)
        {
            _mapper = mapper;
            _userFacade = userFacade;
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
                MobileNumber = User.FindFirstValue("MobileNumber"),
                Role = User.FindFirstValue(ClaimTypes.Role),
                Email = User.FindFirstValue(ClaimTypes.Email)
            };

            return Ok(res);
        }

        [HttpGet("info")]
        public ActionResult GetUserInfo()
        {
            var user = _userFacade.GetUserById(User.GetId());
            return Ok(user);
        }

        [HttpPut("update")]
        public ActionResult UpdateUserInfo(UpdateUserInfoModel model)
        {
            model.UserId = User.GetId();
            _userFacade.UpdateUserInfo(_mapper.Map<UpdateUserInfoDTO>(model));
            return Ok();
        }

        [HttpGet("city")]
        public ActionResult GetUserCity()
        {
            long userId = User.GetId();
            int cityId = _userFacade.GetUserCity(userId);
            return Ok(cityId);
        }

        [HttpPut("set-city/{cityId}")]
        public ActionResult UpdateCityForUser(int cityId)
        {
            long userId = User.GetId();
            _userFacade.UpdateCityForUser(cityId, userId);
            return Ok();
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
