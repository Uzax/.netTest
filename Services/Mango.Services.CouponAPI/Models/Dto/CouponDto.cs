using System.ComponentModel.DataAnnotations;

namespace Mango.Services.CouponAPI.Models.Dto
{
    public class CouponDto
    {
        
        [Length(1,20)]
        [Required]
        public string CouponCode { get; set; }
        
        [Range(1,100)]
        [Required]
        public double DiscountAmount { get; set; }
    }


    
}