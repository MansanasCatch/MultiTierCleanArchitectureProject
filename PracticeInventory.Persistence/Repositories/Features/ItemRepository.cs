using Microsoft.EntityFrameworkCore;
using PracticeInventory.Domain.Entities;
using PracticeInventory.Domain.Interfaces.Features;
using System.Linq;
using System.Linq.Expressions;

namespace PracticeInventory.Persistence.Repositories.Features;

public class ItemRepository : IItemRepository
{
    private readonly ApplicationDbContext _context;

    public ItemRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Items>> GetItems()
    {
       return await _context.Set<Items>().ToListAsync();
    }

    public IQueryable<Items> FindQueryable(Expression<Func<Items, bool>> predicate)
    {
        return _context.Set<Items>().Where(predicate).AsQueryable();
    }

    public async Task<Items> GetByIdAsync(Guid ItemId)
    {
        return await _context.Set<Items>().FindAsync(ItemId);
    }

    public void Add(Items item)
    {
        _context.Set<Items>().Add(item);
    }

    public void Remove(Items item)
    {
        _context.Set<Items>().Remove(item);
    }
}
