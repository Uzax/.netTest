using Mango.Services.AuthAPI.Models.Dto;
using Mango.Services.AuthAPI.Service;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.AuthAPI.Controller
{
    [Route("api/auth")]
    [ApiController]
    public class AuthAPIController : ControllerBase
    {
        
        private readonly IAuthService _authService;
        protected ResponseDto _response;

        public AuthAPIController(IAuthService authService)
        {
            _authService = authService;
            _response = new ();
        }
        
        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegistrationRequestDto registrationRequestDto)
        {
            var errorMessage = await _authService.Register(registrationRequestDto);
            if (!string.IsNullOrEmpty(errorMessage))
            {
                _response.IsSuccess = false;
                _response.Message = errorMessage;
                return BadRequest(errorMessage);
                
            }
            return Ok(_response);
        }
        
        
        [HttpPost("login")]
        public async Task<ActionResult> Login()
        {
            return Ok();
        }
    }
}