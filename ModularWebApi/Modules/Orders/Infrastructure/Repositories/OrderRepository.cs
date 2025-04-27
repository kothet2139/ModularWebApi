using Microsoft.EntityFrameworkCore;
using ModularWebApi.Modules.Orders.Domain.Entities;
using ModularWebApi.Modules.Orders.Domain.Repositories;
using ModularWebApi.SharedKernel.Persistence;

namespace ModularWebApi.Modules.Orders.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _ordersDbContext;
        public OrderRepository(AppDbContext dbContext) => _ordersDbContext = dbContext;
        public async Task AddAsync(Order order)
        {
            await _ordersDbContext.Orders.AddAsync(order);
            await _ordersDbContext.SaveChangesAsync();
        }
        public async Task<Order> GetByIdAsync(Guid id) => await _ordersDbContext.Orders.Include(o => o.Items).FirstOrDefaultAsync(o => o.Id == id);
      
        public async Task UpdateAsync(Order order)
        {
            _ordersDbContext.Orders.Update(order);
            await _ordersDbContext.SaveChangesAsync();
        }
    }
}
