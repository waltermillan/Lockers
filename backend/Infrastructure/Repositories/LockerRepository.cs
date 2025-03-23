using Core.Entities;
using Core.Interfases;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class LockerRepository(Context context) : GenericRepository<Locker>(context), ILockerRepository
{
    public async Task<Locker> GetByLockerIdAsync(int lockerId)
    {
        return await _context.Lockers
                             .FirstOrDefaultAsync(a => a.Id == lockerId);
    }

    public override async Task<Locker> GetByIdAsync(int id)
    {
        return await _context.Lockers
                          .FirstOrDefaultAsync(p => p.Id == id);
    }

    public override async Task<IEnumerable<Locker>> GetAllAsync()
    {
        return await _context.Lockers.ToListAsync();
    }
}
