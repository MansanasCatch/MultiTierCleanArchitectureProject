using PracticeInventory.Domain.Entities;
using System.Linq.Expressions;

namespace PracticeInventory.Domain.Interfaces.Features;

public interface ICategoryRepository
{
    Task<IEnumerable<Categories>> GetCategories();
    IQueryable<Categories> FindQueryable(Expression<Func<Categories, bool>> predicate);
    Task<Categories> GetByIdAsync(Guid CategoryId);
    void Add(Categories category);
    void Remove(Categories category);
}