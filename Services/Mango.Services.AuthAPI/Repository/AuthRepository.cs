using Mango.Services.AuthAPI.Data;
using Mango.Services.AuthAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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

        public async Task<ApplicationUsers> getUserByUsername(string username)
        {
            var result = await _context.Users.FirstOrDefaultAsync(x => x.UserName == username);
            return result;
        }

        public async Task<bool> checkUserPassword(ApplicationUsers user, string password)
        {
            var result = await _userManager.CheckPasswordAsync(user, password);
            
            return result;
        }

        public async Task<IList<string>> GetUserRoles(ApplicationUsers user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            return roles; 
        }

        public async Task setRole(ApplicationUsers user, string role)
        {
            await _userManager.AddToRoleAsync(user, role);
             
        }
    }
}