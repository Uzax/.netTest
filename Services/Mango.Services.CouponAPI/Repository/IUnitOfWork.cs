using Mango.Services.CouponAPI.Models;

namespace Mango.Services.CouponAPI.Repository
{

    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<Coupon> Coupons { get; }
        
        int Complete();
        
        
    }
}