using Domain.Contracts;
using Domain.Contracts.Repositories;
using Domain.Entitties;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public abstract class GenericRepo<T> : IGenericRepo<T> where T : class
    {
        private readonly IAppDbContext _context;
        protected GenericRepo(IAppDbContext context)
        {
            _context = context;
        }

        virtual public async Task Add(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public async Task<bool> Any(Expression<Func<T,bool>> expression)
        {
            return await _context.Set<T>().AnyAsync(expression);
        }

        virtual public async Task Delete(Guid id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if(entity != null)
            {
                _context.Set<T>().Remove(entity);
            }
        }

        virtual public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>>? expression = null)
        {
            if(expression == null)
            {
                return await _context.Set<T>()
                    .AsNoTracking<T>()
                    .ToListAsync();
            }
            return await _context.Set<T>().Where(expression).ToListAsync();
        }

        virtual public async Task<T?> GetById(Guid id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            return entity;
        }

        virtual public Task Update(T entity)
        {
            _context.Set<T>().Update(entity);
            return Task.CompletedTask;
        }
    }
}
