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
    
    Task DeleteCouponByIdAsync(int id);
    Task DeleteCouponByCodeAsync(string code);

    Task<Coupon> GetLastElementAsync(); 
}