using ModularWebApi.Modules.User.Domain.Entities;

namespace ModularWebApi.Modules.User.Domain.Repository
{
    public interface IUserRepository
    {
        Task<Entities.User> GetByIdAsync(Guid id);
        Task<Entities.User> GetByEmailAsync(string email);

        Task AddAsync(Entities.User user);
        Task UpdateAsync(Entities.User user);
    }
}
