using System.ComponentModel.DataAnnotations;

namespace Mango.Services.AuthAPI.Models.Dto
{

    public class RolesDto
    {
        // [Required]
        public string username { get; set; }
        
        // [Required]
        // [RoleCheck(ErrorMessage = "Roles must be a valid role [Admin , User] ")]
        public IList<string> roles { get; set; }
    }
    
    
    // public class RoleCheckAttribute : ValidationAttribute
    // {
    //     
    //     private readonly IList<string> _validRoles = new List<string> { "Admin", "User" };
    //     public override bool IsValid(object value)
    //     {
    //         var roles = value as IList<string>;
    //     
    //         if (roles == null || !roles.All(role => _validRoles.Contains(role)))
    //         {
    //             return false;
    //         }
    //
    //         return true; 
    //     }
    // }
}