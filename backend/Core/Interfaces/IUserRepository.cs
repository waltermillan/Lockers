using Core.Entities;

namespace Core.Interfaces;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User> GetByUserAsync(string user);
}
