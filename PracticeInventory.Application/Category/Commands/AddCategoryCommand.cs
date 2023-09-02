using MediatR;
using PracticeInventory.Core.Results;
using System.ComponentModel.DataAnnotations;

namespace PracticeInventory.Application.Category.Commands;

public class AddCategoryCommand : IRequest<Result<Guid>>
{
    [Required(ErrorMessage = "CategoryName is required")]
    public string CategoryName { get; set; } = string.Empty;
}