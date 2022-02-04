using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestWith.NET.Business;
using RestWith.NET.Data.VO;

namespace RestWith.NET.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class AuthController : ControllerBase
    {
        private readonly ILoginBusiness _loginBusiness;

        public AuthController(ILoginBusiness loginBusiness)
        {
            _loginBusiness = loginBusiness;
        }

        [HttpPost("signin")]
        public IActionResult SignIn ([FromBody] UserVo user)
        {
            if(user==null)
                return BadRequest("Invalid Client Request");
            var token = _loginBusiness.ValidateCredentials(user);
            if(token == null)
                return Unauthorized(); 
            return Ok(token);          
        }

        [HttpPost("refresh")]
        public IActionResult Refresh ([FromBody] TokenVo tokenVo)
        {
            if(tokenVo==null)
                return BadRequest("Invalid Client Request");
            var token = _loginBusiness.ValidateCredentials(tokenVo);
            if(token == null)
                return BadRequest("Invalid Client Request");
            return Ok(token);          
        }

          [HttpGet("revoke")]
          [Authorize("Bearer")]
        public IActionResult Revoke ()
        {
            var username = User.Identity.Name;
            var result = _loginBusiness.RevokeToken(username);
            if(!result)
                return BadRequest("Invalid Client Request");
            return Ok();          
        }

    }
}