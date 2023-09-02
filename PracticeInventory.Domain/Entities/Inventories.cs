using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracticeInventory.Domain.Entities;

public class Inventories
{
    [Key]
    public Guid InventoryId { get; set; }
    [ForeignKey("ItemId")]
    public Guid ItemId { get; set; }
    public float Cost { get; set; }
    public float SalePrice { get; set; }
    public float CurrentQuantity { get; set; }
    public float CriticalQuantity { get; set; }
    public virtual Items Item { get; set; }
}