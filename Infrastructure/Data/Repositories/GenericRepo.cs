using Domain.Contracts;
using Domain.Contracts.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<GenericRepo<T>> _logger;

        protected GenericRepo(IAppDbContext context, ILogger<GenericRepo<T>> logger)
        {
            _context = context;
            _logger = logger;
        }

        virtual public async Task Add(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            _logger.LogInformation($"Added entity of type {typeof(T).Name}");
        }

        public async Task<bool> Any(Expression<Func<T, bool>> expression)
        {
            _logger.LogInformation($"Checking if any entity of type {typeof(T).Name} satisfies the expression {expression}");
            return await _context.Set<T>().AnyAsync(expression);
        }

        virtual public async Task Delete(Guid id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
                _logger.LogInformation($"Deleted entity of type {typeof(T).Name} with ID {id}");
            }
        }

        virtual public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>>? expression = null)
        {
            if (expression == null)
            {
                _logger.LogInformation($"Getting all entities of type {typeof(T).Name}");
                return await _context.Set<T>()
                    .AsNoTracking<T>()
                    .ToListAsync();
            }
            _logger.LogInformation($"Getting entities of type {typeof(T).Name} that satisfy the expression");
            return await _context.Set<T>().Where(expression).ToListAsync();
        }

        virtual public async Task<T?> GetById(Guid id)
        {
            _logger.LogInformation($"Getting entity of type {typeof(T).Name} with ID {id}");
            var entity = await _context.Set<T>().FindAsync(id);
            return entity;
        }

        virtual public Task Update(T entity)
        {
            _context.Set<T>().Update(entity);
            _logger.LogInformation($"Updated entity of type {typeof(T).Name}");
            return Task.CompletedTask;
        }
    }
}
