using MediatR;
using PracticeInventory.Core.Results;
using System.ComponentModel.DataAnnotations;

namespace PracticeInventory.Application.Inventory.Commands;

public class UpdateInventoryCommand : IRequest<Result<Guid>>
{
    [Required(ErrorMessage = "InventoryId is required")]
    public Guid InventoryId { get; set; }
    [Required(ErrorMessage = "ItemId is required")]
    public Guid ItemId { get; set; }
    [Required(ErrorMessage = "Cost is required")]
    public float Cost { get; set; }
    [Required(ErrorMessage = "SalePrice is required")]
    public float SalePrice { get; set; }
    [Required(ErrorMessage = "CurrentQuantity is required")]
    public float CurrentQuantity { get; set; }
    [Required(ErrorMessage = "CriticalQuantity is required")]
    public float CriticalQuantity { get; set; }
}
