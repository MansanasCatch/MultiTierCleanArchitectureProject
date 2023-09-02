namespace PracticeInventory.Application.Inventory.DTO;

public class InventoryDTO
{
    public Guid InventoryId { get; set; }
    public Guid ItemId { get; set; }
    public string ItemName { get; set; }
    public float Cost { get; set; }
    public float SalePrice { get; set; }
    public float CurrentQuantity { get; set; }
    public float CriticalQuantity { get; set; }
}
