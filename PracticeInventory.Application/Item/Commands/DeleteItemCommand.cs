using MediatR;
using PracticeInventory.Core.Results;
using System.ComponentModel.DataAnnotations;

namespace PracticeInventory.Application.Item.Commands;

public class DeleteItemCommand : IRequest<Result<Unit>>
{
    [Required(ErrorMessage = "ItemId is required")]
    public Guid ItemId { get; set; }
}
