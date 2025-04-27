﻿using ModularWebApi.Modules.Orders.Domain.Entities;

namespace ModularWebApi.Modules.Orders.Domain.Repositories
{
    public interface IOrderRepository
    {
        Task AddAsync(Order order);
        Task UpdateAsync(Order order);
        Task<Order> GetByIdAsync(Guid id);
    }
}
