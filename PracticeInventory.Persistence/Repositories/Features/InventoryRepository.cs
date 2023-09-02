using Microsoft.EntityFrameworkCore;
using PracticeInventory.Domain.Entities;
using PracticeInventory.Domain.Interfaces.Features;
using System.Linq.Expressions;

namespace PracticeInventory.Persistence.Repositories.Features;

public class InventoryRepository : IInventoryRepository
{
    private readonly ApplicationDbContext _context;

    public InventoryRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Inventories>> GetInventories()
    {
        return await _context.Set<Inventories>().ToListAsync();
    }

    public IQueryable<Inventories> FindQueryable(Expression<Func<Inventories, bool>> predicate)
    {
        return _context.Set<Inventories>().Where(predicate).AsQueryable();
    }

    public async Task<Inventories> GetByIdAsync(Guid InventoryId)
    {
        return await _context.Set<Inventories>().FindAsync(InventoryId);
    }
    public void Add(Inventories inventory)
    {
        _context.Set<Inventories>().Add(inventory);
    }

    public void Remove(Inventories inventory)
    {
        _context.Set<Inventories>().Remove(inventory);
    }
}
