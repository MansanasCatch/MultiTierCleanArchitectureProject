using PracticeInventory.Domain.Entities;
using System.Linq.Expressions;

namespace PracticeInventory.Domain.Interfaces.Features;

public interface IItemRepository
{
    Task<IEnumerable<Items>> GetItems();
    IQueryable<Items> FindQueryable(Expression<Func<Items, bool>> predicate);
    Task<Items> GetByIdAsync(Guid ItemId);
    void Add(Items category);
    void Remove(Items category);
}
