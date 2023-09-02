using Microsoft.EntityFrameworkCore;
using PracticeInventory.Domain.Entities;
using PracticeInventory.Domain.Interfaces.Features;
using System.Linq.Expressions;

namespace PracticeInventory.Persistence.Repositories.Features;

internal sealed class CategoryRepository : ICategoryRepository
{
    private readonly ApplicationDbContext _context;

    public CategoryRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Categories>> GetCategories()
    {
        return await _context.Set<Categories>().ToListAsync();
    }

    public IQueryable<Categories> FindQueryable(Expression<Func<Categories, bool>> predicate)
    {
        return _context.Set<Categories>().Where(predicate).AsQueryable();
    }

    public async Task<Categories> GetByIdAsync(Guid CategoryId)
    {
        return await _context.Set<Categories>().FindAsync(CategoryId);
    }

    public void Add(Categories category)
    {
        _context.Set<Categories>().Add(category);
    }

    public void Remove(Categories category)
    {
        _context.Set<Categories>().Remove(category);
    }
}