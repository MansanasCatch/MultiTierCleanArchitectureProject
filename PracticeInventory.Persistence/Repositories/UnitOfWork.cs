using PracticeInventory.Domain.Interfaces;
using PracticeInventory.Domain.Interfaces.Features;
using PracticeInventory.Persistence.Repositories.Features;

namespace PracticeInventory.Persistence.Repositories;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        CategoryRepository = new CategoryRepository(context);
        ItemRepository = new ItemRepository(context);
        InventoryRepository = new InventoryRepository(context);
    }
    public ICategoryRepository CategoryRepository { get; private set; }
    public IItemRepository ItemRepository { get; private set; }
    public IInventoryRepository InventoryRepository { get; private set; }

    public async Task<int> CompleteAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}