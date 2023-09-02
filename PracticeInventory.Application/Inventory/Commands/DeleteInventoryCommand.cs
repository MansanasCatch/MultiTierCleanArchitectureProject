using MediatR;
using PracticeInventory.Core.Results;
using System.ComponentModel.DataAnnotations;

namespace PracticeInventory.Application.Inventory.Commands;

public class DeleteInventoryCommand : IRequest<Result<Unit>>
{
    [Required(ErrorMessage = "InventoryId is required")]
    public Guid InventoryId { get; set; }
}
