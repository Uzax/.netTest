using Mango.Services.AuthAPI.Data;
using Mango.Services.AuthAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace Mango.Services.AuthAPI.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUsers> _userManager;


        public AuthRepository(AppDbContext context, UserManager<ApplicationUsers> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        
        public async Task<IdentityResult> createNewUser(ApplicationUsers user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            return result;
            
        }
    }
}