using MediatR;
using PracticeInventory.Application.Category.DTO;

namespace PracticeInventory.Application.Category.Queries;

public class GetCategoriesQuery : IRequest<IEnumerable<CategoryDTO>>
{
}
