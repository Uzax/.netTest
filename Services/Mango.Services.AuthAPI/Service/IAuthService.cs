using Mango.Services.AuthAPI.Models.Dto;

namespace Mango.Services.AuthAPI.Service
{
    public interface IAuthService
    {
    Task<string> Register(RegistrationRequestDto registrationRequestDto);
    Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
    }

}