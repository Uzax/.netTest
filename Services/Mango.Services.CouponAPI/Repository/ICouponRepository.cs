using Mango.Services.CouponAPI.Models;
using Mango.Services.CouponAPI.Models.Dto;

namespace Mango.Services.CouponAPI.Repository;

public interface ICouponRepository
{
    Task<IEnumerable<Coupon>> GetCouponsAsync();
    
    Task<Coupon> GetCouponByIdAsync(int couponId);
    
    Task<Coupon> GetCouponByCodeAsync(string CouponCode);
    
    Task AddCouponAsync(Coupon coupon);
    
    Task UpdateCouponAsync(Coupon coupon);
    
    Task DeleteCouponAsync(Coupon coupon);

    Task<Coupon> GetLastElementAsync(); 
}