using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts.Repositories
{
    public interface IGenericRepo<T> where T : class
    {
        Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>>? expression = null);
        Task<T> GetById(Guid id);
        Task Add(T entity);
        Task Update(T entity);
        Task Delete(int id);
    }
}
