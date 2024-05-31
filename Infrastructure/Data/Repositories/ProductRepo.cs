﻿using Domain.Contracts;
using Domain.Contracts.Repositories;
using Domain.Entitties;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class ProductRepo : GenericRepo<Product>, IProductRepo
    {
        private readonly IAppDbContext _context;
        public ProductRepo(IAppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Product?> GetProductByName(string name)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Name == name);
        }
        public async Task<Tuple<IEnumerable<Product>, int>> GetAll(int page, int pageSize, string? filter, ProductFilter? filterBy, ProductSort? sortBy, bool descending)

        {
            var query = _context.Products
                        .AsNoTracking()
                        .AsQueryable();

            if (filterBy != null && filter != null)
            {
                switch (filterBy)
                {
                    case ProductFilter.Name:
                        query = query.Where(p => p.Name.Contains(filter));
                        break;
                    case ProductFilter.Description:
                        query = query.Where(p => p.Description.Contains(filter));
                        break;
                    case ProductFilter.Merchant:
                        query = query.Where(p => p.Merchant.Contains(filter));
                        break;
                    default:
                        break;
                }
            }

            if (sortBy != null)
            {
                switch (sortBy)
                {
                    case ProductSort.Price:
                        query = descending ? query.OrderByDescending(p=> p.Price) : query.OrderBy(p => p.Price);
                        break;
                }
            }

            var totalCount = await query.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            var products = await query.Skip((page - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToListAsync();

            var combinedRet = new Tuple<IEnumerable<Product>, int>(products, totalPages);

            return combinedRet;
        }
    }
}
