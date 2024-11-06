using System.Linq.Expressions;

namespace Mango.Services.CouponAPI.Repository
{

    public interface IBaseRepository<T> where T : class
    {

        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> AddAsync(T entity);
        T Update(T entity);
        void Delete(T entity);
        Task<T> Find(Expression<Func<T, bool>> criteria, string[] includes = null);
        
    }
}