using Mango.Services.AuthAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace Mango.Services.AuthAPI.Repository
{

    public interface IAuthRepository
    {

        Task<IdentityResult> createNewUser(ApplicationUsers user , string password);
        
        Task<ApplicationUsers> getUserByUsername(string username);
        
        Task<bool> checkUserPassword(ApplicationUsers user , string password);

    }
}