using PracticeInventory.Domain.Entities;

namespace PracticeInventory.Application.Category.DTO;

public class CategoryDTO
{
    public Guid CategoryId { get; set; }
    public string CategoryName { get; set; }

    public static CategoryDTO ToCategoryDTOMapped(Categories source)
    {
        if (source is null)
        {
            throw new InvalidOperationException("Cannot mapped Category to DTO");
        }

        return new CategoryDTO
        {
            CategoryId = source.CategoryId,
            CategoryName = source.CategoryName
        };
    }

    public static IEnumerable<CategoryDTO> ToCategoryDTOMappedList(IEnumerable<Categories> source)
    {
        if (source is null)
        {
            throw new InvalidOperationException("Cannot mapped Category to DTO");
        }

        return source.Select(item => new CategoryDTO
        {
            CategoryId = item.CategoryId,
            CategoryName = item.CategoryName
        });
    }
}
