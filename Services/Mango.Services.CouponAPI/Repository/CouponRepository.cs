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
            var coupon =  await _context.Coupons.Where(c => c.CouponId == couponId).FirstOrDefaultAsync();
            return coupon;
           
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

        public async Task UpdateCouponAsync(Coupon coupon)
        {
            var trackedEntity = await _context.Coupons.AsNoTracking()
                .FirstOrDefaultAsync(c => c.CouponId == coupon.CouponId);
                          
            if (trackedEntity != null)
            {
                _context.Entry(trackedEntity).State = EntityState.Detached;
            }
    
             _context.Coupons.Update(coupon);
            await _context.SaveChangesAsync();
        }

        public Task DeleteCouponByIdAsync(int id)
        {
            _context.Coupons.Remove( _context.Coupons.Where(coupon => coupon.CouponId == id).FirstOrDefault());
            _context.SaveChanges();
            return Task.CompletedTask;
        }

        public Task DeleteCouponByCodeAsync(string code)
        {
            _context.Coupons.Remove( _context.Coupons.Where(coupon => coupon.CouponCode == code).FirstOrDefault());
            _context.SaveChanges();
            return Task.CompletedTask;
        }


        public Task<Coupon> GetLastElementAsync()
        {
            return _context.Coupons.OrderByDescending(o => o.CouponId).FirstOrDefaultAsync();
        }
    }
}