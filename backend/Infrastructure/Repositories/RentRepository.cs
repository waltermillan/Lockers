using Core.Entities;
using Core.Interfases;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class RentRepository(Context context) : GenericRepository<Rent>(context), IRentRepository
{

    // Método existente
    public override async Task<Rent> GetByIdAsync(int id)
    {
        return await _context.Rents
                          .FirstOrDefaultAsync(p => p.Id == id);
    }

    // Método existente
    public override async Task<IEnumerable<Rent>> GetAllAsync()
    {
        return await _context.Rents.ToListAsync();
    }
}