using PracticeInventory.Domain.Entities;
using System.Linq.Expressions;

namespace PracticeInventory.Domain.Interfaces.Features;

public interface IInventoryRepository
{
    Task<IEnumerable<Inventories>> GetInventories();
    IQueryable<Inventories> FindQueryable(Expression<Func<Inventories, bool>> predicate);
    Task<Inventories> GetByIdAsync(Guid InventoryId);
    void Add(Inventories inventory);
    void Remove(Inventories inventory);
}
