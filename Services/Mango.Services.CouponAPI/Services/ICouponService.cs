using Mango.Services.CouponAPI.Models;
using Mango.Services.CouponAPI.Models.Dto;

namespace Mango.Services.CouponAPI.Services
{
    public interface ICouponService
    {
        
        Task<ResponseDto> GetAllCouponsAsync();
        
        Task<ResponseDto> GetCouponByIdAsync(int couponId);
        Task<ResponseDto> GetCouponByCode(string CouponCode);
        
        Task <ResponseDto> AddCouponAsync(CouponDto couponDto);
        
        Task<ResponseDto> DeleteCouponbyIdAsync(int id);
        Task<ResponseDto> DeleteCouponbyCodeAsync(string code);
        
        
        Task<ResponseDto> UpdateCouponAsync(CouponDto couponDto , int id);
    }
}