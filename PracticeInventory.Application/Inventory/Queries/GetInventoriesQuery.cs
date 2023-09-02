using MediatR;
using PracticeInventory.Application.Inventory.DTO;
using PracticeInventory.Core.Results;

namespace PracticeInventory.Application.Inventory.Queries;

public class GetInventoriesQuery : IRequest<Result<IEnumerable<InventoryDTO>>>
{
}
