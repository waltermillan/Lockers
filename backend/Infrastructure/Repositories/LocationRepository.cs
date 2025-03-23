using Core.Entities;
using Core.Interfases;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class LocationRepository(Context context) : GenericRepository<Location>(context), ILocationRepository
{
    public async Task<Location> GetByLocationIdAsync(int locationId)
    {
        return await _context.Locations
                             .FirstOrDefaultAsync(a => a.Id == locationId);
    }

    public override async Task<Location> GetByIdAsync(int id)
    {
        return await _context.Locations
                          .FirstOrDefaultAsync(p => p.Id == id);
    }

    public override async Task<IEnumerable<Location>> GetAllAsync()
    {
        return await _context.Locations.ToListAsync();
    }
}
