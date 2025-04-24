using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class DocumentRepository(Context context) : GenericRepository<Document>(context), IDocumentRepository
{

    public override async Task<Document> GetByIdAsync(int id)
    {
        return await _context.Documents
                          .FirstOrDefaultAsync(p => p.Id == id);
    }

    public override async Task<IEnumerable<Document>> GetAllAsync()
    {
        return await _context.Documents.ToListAsync();
    }
}