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
        
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthService(
            RoleManager<IdentityRole> roleManager ,  IAuthRepository authRepository , IJwtTokenGenerator jwtTokenGenerator)
        {
            _roleManager = roleManager;
            _authRepository = authRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
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

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
           var user = await _authRepository.getUserByUsername(loginRequestDto.Username);
           var isValid = await _authRepository.checkUserPassword(user, loginRequestDto.Password);

           
           // if user is Password is Wrong 
           if (user == null || !isValid)
           {
               return new LoginResponseDto()
               {
                   // User = null,
                   Token = ""
               };
           }

         
              
               // if user is Found . Genreate Token (TODO) 
               
               //Generate Token 
               var token = _jwtTokenGenerator.GenerateToken(user);

               UserDto userDto = new()
               {
                   ID = user.Id,
                   Username = user.UserName,
                   Name = user.name,
                   PhoneNumber = user.PhoneNumber,
               };

               LoginResponseDto loginResponseDto = new()
               {
                   
                   Token = token
               };

               return loginResponseDto; 



           }
        }
    }
