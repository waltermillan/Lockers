using Core.Entities;

namespace Core.Interfases;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User> AuthenticateAsync(string userName, string password);  // Agrega este método
}
