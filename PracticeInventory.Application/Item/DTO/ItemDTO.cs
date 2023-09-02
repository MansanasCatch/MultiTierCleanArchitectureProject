using PracticeInventory.Application.Category.DTO;
using PracticeInventory.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracticeInventory.Application.Item.DTO;

public class ItemDTO
{
    public Guid ItemId { get; set; }
    public string ItemName { get; set; }
    public string CategoryName { get; set; }
    public static IEnumerable<ItemDTO> ToItemDTOMappedList(IEnumerable<Items> source)
    {
        if (source is null)
        {
            throw new InvalidOperationException("Cannot mapped Item to DTO");
        }

        return source.Select(item => new ItemDTO
        {
            ItemId = item.ItemId,
            ItemName = item.ItemName,
            CategoryName = item.Category.CategoryName
        });
    }
}