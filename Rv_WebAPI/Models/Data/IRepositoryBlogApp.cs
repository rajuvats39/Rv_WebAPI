using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Rv_WebAPI.Models.Data
{
    public interface IRepositoryBlogApp<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter);
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        void Update(T entity);
        Task DeleteAsync(int id);
        Task SaveChangesAsync();
    }
}