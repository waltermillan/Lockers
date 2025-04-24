using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository(Context context) : GenericRepository<User>(context), IUserRepository
{
    public async Task<User> GetByUserAsync(string userName)
    {
        // Search administrator by user
        return await _context.Users
                             .FirstOrDefaultAsync(a => a.UserName == userName);
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
