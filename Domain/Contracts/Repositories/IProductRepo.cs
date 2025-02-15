using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts.Repositories
{
    public interface IProductRepo : IGenericRepo<Product>
    {
        Task<Product?> GetProductByName(string name);
        Task<(IEnumerable<Product>, int)> GetAll(int page, int pageSize, string? filter, ProductFilter? filterBy, ProductSort? sortBy, bool descending);
    }
}
