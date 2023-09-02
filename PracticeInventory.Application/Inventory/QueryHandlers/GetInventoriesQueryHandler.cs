using Dapper;
using MediatR;
using PracticeInventory.Application.Inventory.DTO;
using PracticeInventory.Application.Inventory.Queries;
using PracticeInventory.Core.Results;
using PracticeInventory.Domain.Interfaces;

namespace PracticeInventory.Application.Inventory.QueryHandlers;

public class GetInventoriesQueryHandler : IRequestHandler<GetInventoriesQuery, Result<IEnumerable<InventoryDTO>>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;
    public GetInventoriesQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<IEnumerable<InventoryDTO>>> Handle(GetInventoriesQuery request, CancellationToken cancellationToken)
    {
        //_logger.LogInformation("Starting to do slow work");
        using var connection = _sqlConnectionFactory.Create();
        const string sql = @"SELECT inv.InventoryId,
	                           items.ItemId,
	                           items.ItemName,
                               inv.Cost,
	                           inv.SalePrice,
	                           inv.CurrentQuantity,
	                           inv.CriticalQuantity
                               FROM Inventories as inv
	                           LEFT JOIN Items as items on inv.ItemId = items.ItemId";
        var users = await connection.QueryAsync<InventoryDTO>(sql);
        return Result<IEnumerable<InventoryDTO>>.Success(users);
    }
}