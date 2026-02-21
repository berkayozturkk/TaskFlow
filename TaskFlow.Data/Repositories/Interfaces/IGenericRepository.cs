using System.Linq.Expressions;
using TaskFlow.Models.Enums;

namespace TaskFlow.Data.Repositories.Interfaces;

public interface IGenericRepository<T> where T : class
{
    Task<T?> GetByIdAsync(int id);                  
    Task<IEnumerable<T>> GetAllAsync();              
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
    System.Threading.Tasks.Task AddAsync(T entity);
    System.Threading.Tasks.Task Update(T entity);
    System.Threading.Tasks.Task Remove(T entity);                             
}
