using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.Models;

namespace Mango.Services.CouponAPI.Repository
{

    public class UnitOfWork : IUnitOfWork
    {
       private readonly AppDbContext _context;
       public IBaseRepository<Coupon> Coupons { get; private set; }

       public UnitOfWork(AppDbContext context)
       {
           _context = context;
           Coupons = new BaseRepository<Coupon>(_context);
       }
       
       
       
       public int Complete()
       {
           return _context.SaveChanges();
       }
       
       
       public void Dispose()
       {
           _context.Dispose();
       }
       
    }
}