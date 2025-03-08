using Core.Entities;
using Core.Interfases;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CustomerRepository(Context context) : GenericRepository<Customer>(context), ICustomerRepository
{
    public async Task<Customer> GetByCustomerIdAsync(int customerId)
    {
        return await _context.Customers
                             .FirstOrDefaultAsync(a => a.Id == customerId);
    }

    // Método existente
    public override async Task<Customer> GetByIdAsync(int id)
    {
        return await _context.Customers
                          .FirstOrDefaultAsync(p => p.Id == id);
    }

    // Método existente
    public override async Task<IEnumerable<Customer>> GetAllAsync()
    {
        return await _context.Customers.ToListAsync();
    }
}
