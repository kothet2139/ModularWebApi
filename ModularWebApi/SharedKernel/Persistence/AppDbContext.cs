using MediatR;
using Microsoft.EntityFrameworkCore;
using ModularWebApi.Modules.Orders.Domain.Entities;
using ModularWebApi.Modules.User.Domain.Entities;

namespace ModularWebApi.SharedKernel.Persistence
{
    public class AppDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<User> Users { get; set; }
        private IMediator _mediator;

        public AppDbContext(DbContextOptions<AppDbContext> options, IMediator mediator) : base(options) 
        {
            _mediator = mediator;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().HasMany(o => o.Items).WithOne().OnDelete(DeleteBehavior.Cascade);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var domainEvents = ChangeTracker.Entries<IHasDomainEvents>()
                .SelectMany(e => e.Entity.DomainEvents).ToList();

            var result = await base.SaveChangesAsync();

            foreach (var domainEvent in domainEvents) { 
                await _mediator.Publish(domainEvent);
            }

            return result;
        }
    }
}
