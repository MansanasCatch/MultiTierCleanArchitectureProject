using MediatR;
using PracticeInventory.Application.Category.DTO;
using PracticeInventory.Core.Results;
using System.ComponentModel.DataAnnotations;

namespace PracticeInventory.Application.Category.Queries;

public class GetCategoryQuery : IRequest<Result<CategoryDTO>>
{
    [Required(ErrorMessage = "CategoryId is required")]
    public Guid CategoryId { get; set; }
}