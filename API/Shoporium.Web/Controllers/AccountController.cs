using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shoporium.Business.Logins;
using Shoporium.Entities.DTO.Account;
using Shoporium.Web.Models.Account;

namespace Shoporium.Web.Controllers
{
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
        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginModel request)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var tokens = _loginFacade.Authenticate(_mapper.Map<LoginModelDTO>(request), GetIpAddress());

            return Ok(new LoginResult(tokens.accessToken, tokens.refreshToken));
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
