﻿using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts.Repositories
{
    public interface IOrderRepo : IGenericRepo<Order>
    {
        Task<Tuple<IEnumerable<Order>, int>> GetAll(int page, int pageSize, string? filter, OrderFilter? filterBy, OrderSort? sortBy, bool descending = false);
    }
}
