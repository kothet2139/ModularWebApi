using Microsoft.EntityFrameworkCore;
using ModularWebApi.Modules.User.Domain.Enum;
using ModularWebApi.Modules.User.Domain.Repository;
using ModularWebApi.SharedKernel.Persistence;

namespace ModularWebApi.Modules.User.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _dbContext;
        public UserRepository(AppDbContext dbContext) 
        { 
            _dbContext = dbContext;
        }

        public async Task AddAsync(Domain.Entities.User user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Domain.Entities.User user)
        {
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Domain.Entities.User> GetByEmailAsync(string email)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email!.ToLower() == email.ToLower() && u.Status == UserStatus.Active)
                ?? throw new InvalidOperationException("User not found");
        }

        public async Task<Domain.Entities.User> GetByIdAsync(Guid id)
        {
            return await _dbContext.Users.FindAsync(id) ?? throw new InvalidOperationException("User not found.");
        }
    }
}
