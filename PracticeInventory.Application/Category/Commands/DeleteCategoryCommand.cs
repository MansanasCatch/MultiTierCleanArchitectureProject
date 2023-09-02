using MediatR;
using PracticeInventory.Core.Results;
using System.ComponentModel.DataAnnotations;

namespace PracticeInventory.Application.Category.Commands;

public class DeleteCategoryCommand : IRequest<Result<Unit>>
{
    [Required(ErrorMessage = "CategoryId is required")]
    public Guid CategoryId { get; set; }
}
