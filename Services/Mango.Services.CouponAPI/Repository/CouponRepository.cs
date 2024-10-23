using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.Models;
using Mango.Services.CouponAPI.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.CouponAPI.Repository
{

    public class CouponRepository : ICouponRepository
    {
        private readonly AppDbContext _context;
        
        public CouponRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Coupon>> GetCouponsAsync()
        {
            return await _context.Coupons.ToListAsync();
        }

        public async Task<Coupon> GetCouponByIdAsync(int couponId)
        {
            return await _context.Coupons.FindAsync(couponId);
           
        }

        public async Task<Coupon> GetCouponByCodeAsync(string CouponCode)
        {
            Coupon res =  await _context.Coupons
                .FirstOrDefaultAsync(co => co.CouponCode.ToLower() == CouponCode.ToLower() );
            return res;
        }

        public Task AddCouponAsync(Coupon coupon)
        {
           _context.Coupons.Add(coupon);
           _context.SaveChanges();
           return Task.CompletedTask;
        }

        public Task UpdateCouponAsync(Coupon coupon)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCouponAsync(Coupon coupon)
        {
            throw new NotImplementedException();
        }

        public Task<Coupon> GetLastElementAsync()
        {
            return _context.Coupons.OrderByDescending(o => o.CouponId).FirstOrDefaultAsync();
        }
    }
}