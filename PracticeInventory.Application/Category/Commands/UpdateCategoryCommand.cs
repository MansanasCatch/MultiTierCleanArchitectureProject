using MediatR;
using PracticeInventory.Core.Results;
using System.ComponentModel.DataAnnotations;

namespace PracticeInventory.Application.Category.Commands;

public class UpdateCategoryCommand : IRequest<Result<Guid>>
{
    [Required(ErrorMessage = "CategoryId is required")]
    public Guid CategoryId { get; set; }
    [Required(ErrorMessage = "CategoryName is required")]
    public string CategoryName { get; set; } = string.Empty;
}