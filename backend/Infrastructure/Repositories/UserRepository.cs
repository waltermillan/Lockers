using Core.Entities;
using Core.Interfases;
using Corer.Helpers;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository(Context context) : GenericRepository<User>(context), IUserRepository
{
    public async Task<User> AuthenticateAsync(string userName, string password)
    {
        var user = await _context.Users
                                 .FirstOrDefaultAsync(u => u.UserName.ToLower() == userName.ToLower());

        if (user is null)
            return null;

        if (PasswordHasher.VerifyPassword(password, user.Password)) 
            return user;

        return null; // ==> If the passwords do not match, we return null
    }

    public async Task<User> GetByAdministratorIdAsync(int administratorId)
    {
        return await _context.Users
                             .FirstOrDefaultAsync(a => a.Id == administratorId);
    }

    public override async Task<User> GetByIdAsync(int id)
    {
        return await _context.Users
                          .FirstOrDefaultAsync(p => p.Id == id);
    }

    public override async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _context.Users.ToListAsync();
    }
}
