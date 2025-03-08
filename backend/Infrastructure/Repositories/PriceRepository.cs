using Core.Entities;
using Core.Interfases;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PriceRepository(Context context) : GenericRepository<Price>(context), IPriceRepository
{

    // Método existente
    public override async Task<Price> GetByIdAsync(int id)
    {
        return await _context.Prices
                          .FirstOrDefaultAsync(p => p.Id == id);
    }

    // Método existente
    public override async Task<IEnumerable<Price>> GetAllAsync()
    {
        return await _context.Prices.ToListAsync();
    }
}