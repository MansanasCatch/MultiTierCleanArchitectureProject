using MediatR;
using PracticeInventory.Application.Item.DTO;
using PracticeInventory.Core.Results;

namespace PracticeInventory.Application.Item.Queries;

public class GetItemsQuery : IRequest<Result<IEnumerable<ItemDTO>>>
{
}
