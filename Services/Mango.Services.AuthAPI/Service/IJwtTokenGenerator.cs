using Mango.Services.AuthAPI.Models;

namespace Mango.Services.AuthAPI.Service
{

    public interface IJwtTokenGenerator
    {
        
        string GenerateToken(ApplicationUsers applicationUser ,  IList<string> roles);
        
        
        

    }
}