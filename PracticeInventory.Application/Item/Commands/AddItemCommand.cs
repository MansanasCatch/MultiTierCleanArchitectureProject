using MediatR;
using PracticeInventory.Core.Results;
using System.ComponentModel.DataAnnotations;

namespace PracticeInventory.Application.Item.Commands;

public class AddItemCommand : IRequest<Result<Guid>>
{
    [Required(ErrorMessage = "CategoryId is required")]
    public Guid CategoryId { get; set; }
    [Required(ErrorMessage = "ItemName is required")]
    public string ItemName { get; set; }
}
