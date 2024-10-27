using Mango.Services.AuthAPI.Data;
using Mango.Services.AuthAPI.Models;
using Mango.Services.AuthAPI.Models.Dto;
using Mango.Services.AuthAPI.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.AuthAPI.Service
{

    public class AuthService : IAuthService
    {
       
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAuthRepository _authRepository;

        public AuthService(
            RoleManager<IdentityRole> roleManager ,  IAuthRepository authRepository)
        {
            _roleManager = roleManager;
            _authRepository = authRepository;
        }
        
        
        
        public async  Task<string> Register(RegistrationRequestDto registrationRequestDto)
        {
            ApplicationUsers user = new()
            {
                UserName = registrationRequestDto.Username,
                Email = registrationRequestDto.Username,
                NormalizedUserName = registrationRequestDto.Username.ToUpper(),
                name = registrationRequestDto.Name,
                PhoneNumber = registrationRequestDto.PhoneNumber,
            };

            try
            {

                var reault =await  _authRepository.createNewUser(user , registrationRequestDto.Password);

                if (reault.Succeeded)
                {
                    
                    
                    return "";
                }
                else
                {
                    return reault.Errors.FirstOrDefault().Description;
                }
            }
            catch (Exception e)
            {
                return e.Message.FirstOrDefault().ToString();
            }
            
            
        }

        public Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            throw new NotImplementedException();
        }
    }
}