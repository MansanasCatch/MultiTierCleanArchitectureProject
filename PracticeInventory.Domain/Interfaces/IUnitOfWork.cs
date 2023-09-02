using PracticeInventory.Domain.Interfaces.Features;

namespace PracticeInventory.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    ICategoryRepository CategoryRepository { get; }
    IItemRepository ItemRepository { get; }
    IInventoryRepository InventoryRepository { get; }
    Task<int> CompleteAsync(CancellationToken cancellationToken = default);
}