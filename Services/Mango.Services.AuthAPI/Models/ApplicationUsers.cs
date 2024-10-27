using Microsoft.AspNetCore.Identity;

namespace Mango.Services.AuthAPI.Models
{

    public class ApplicationUsers : IdentityUser
    {
        public string name { get; set; }
    }
}