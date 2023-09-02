using MediatR;
using PracticeInventory.Application.Inventory.Commands;
using PracticeInventory.Core.Results;
using PracticeInventory.Domain.Entities;
using PracticeInventory.Domain.Interfaces;

namespace PracticeInventory.Application.Inventory.CommandHandlers;

public class AddInventoryCommandHandler : IRequestHandler<AddInventoryCommand, Result<Guid>>
{
    private readonly IUnitOfWork _unitOfWork;
    public AddInventoryCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<Guid>> Handle(AddInventoryCommand request, CancellationToken cancellationToken)
    {
        var inventory = new Inventories()
        {
            InventoryId = Guid.NewGuid(),
            ItemId = request.ItemId,
            Cost = request.Cost,
            SalePrice = request.SalePrice,
            CurrentQuantity = request.CurrentQuantity,
            CriticalQuantity = request.CriticalQuantity
        };
        _unitOfWork.InventoryRepository.Add(inventory);

        var result = await _unitOfWork.CompleteAsync() > 0;

        if (!result) return Result<Guid>.Failure("Failed to add inventory.");

        return Result<Guid>.Success(inventory.InventoryId);
    }
}
