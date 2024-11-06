using Mango.Services.AuthAPI.Messaging.Publisher;
using Mango.Services.AuthAPI.Models.Dto;
using Mango.Services.AuthAPI.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.AuthAPI.Controller
{
    [Route("api/auth")]
    [ApiController]
    public class AuthAPIController : ControllerBase
    {
        
        private readonly IAuthService _authService;
        protected ResponseDto _response;

        private readonly IMessageBusClient _messageBus; 

        public AuthAPIController(IAuthService authService , IMessageBusClient messageBus)
        {
            _authService = authService;
            _response = new ();
            _messageBus = messageBus; 
        }
        
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDto registrationRequestDto)
        {
            var errorMessage = await _authService.Register(registrationRequestDto);
            if (!string.IsNullOrEmpty(errorMessage))
            {
                _response.IsSuccess = false;
                _response.Message = errorMessage;
                return Unauthorized(errorMessage);
                
            }
           
            return Ok(_response);
        }
        
        
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var publushDto = new LoginRequestPublishDto()
            {
                serviceName = "Auth/Login",
                Time = DateTime.Now,
                fromIP = HttpContext.Connection.RemoteIpAddress?.ToString(),
                Username = loginRequestDto.Username
            }; 
            var loginResult = await _authService.Login(loginRequestDto);
            if (loginResult.Token == "")
            {

                try
                {
                    publushDto.Message = "Failed Login Attempt"; 
                    _messageBus.publishNewLoginAttempt(publushDto);

                }
                catch (Exception e)
                {
                    Console.WriteLine($"----> Failed to Send RabbitMQ from Controller , msg = {e.Message}");
                   
                }
                
                _response.IsSuccess = false;
                _response.Message = "Username or password is incorrect.";
                return Unauthorized(_response);

            }
           
            
            try
            {
                publushDto.Message = "Succesful Login Attempt"; 
                _messageBus.publishNewLoginAttempt(publushDto);

            }
            catch (Exception e)
            {
                Console.WriteLine($"----> Failed to Send RabbitMQ from Controller , msg = {e.Message}");
            }
            _response.Result = loginResult; 
            
            return Ok(_response);

        }

        
        [HttpPost("setRole")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> setRoles([FromBody] RolesDto rolesDto)
        {
            var result = await _authService.setRoles(rolesDto.username , rolesDto.roles);
            if (result != null)
            {
                _response.IsSuccess = true;
                _response.Message = result;
                return Ok(_response);
            }
            _response.IsSuccess = false;
            _response.Message = result;
            return BadRequest(_response); 
        }
        
    }
}