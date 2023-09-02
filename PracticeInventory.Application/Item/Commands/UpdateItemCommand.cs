using MediatR;
using PracticeInventory.Core.Results;
using System.ComponentModel.DataAnnotations;

namespace PracticeInventory.Application.Item.Commands;

public class UpdateItemCommand : IRequest<Result<Guid>>
{
    [Required(ErrorMessage = "ItemId is required")]
    public Guid ItemId { get; set; }
    [Required(ErrorMessage = "CategoryId is required")]
    public Guid CategoryId { get; set; }
    [Required(ErrorMessage = "ItemName is required")]
    public string ItemName { get; set; }
}