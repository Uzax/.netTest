using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.AuthAPI.Controller
{
    [Route("api/auth")]
    [ApiController]
    public class AuthAPIController : ControllerBase
    {
        [HttpPost("register")]
        public async Task<ActionResult> Register()
        {
            return Ok();
        }
        
        
        [HttpPost("login")]
        public async Task<ActionResult> Login()
        {
            return Ok();
        }
    }
}