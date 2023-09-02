using MediatR;
using PracticeInventory.Application.Inventory.Commands;
using PracticeInventory.Core.Results;
using PracticeInventory.Domain.Entities;
using PracticeInventory.Domain.Interfaces;

namespace PracticeInventory.Application.Inventory.CommandHandlers;

public class UpdateInventoryCommandHandler : IRequestHandler<UpdateInventoryCommand, Result<Guid>>
{
    private readonly IUnitOfWork _unitOfWork;
    public UpdateInventoryCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<Guid>> Handle(UpdateInventoryCommand request, CancellationToken cancellationToken)
    {
        var findInventory = await _unitOfWork.InventoryRepository.GetByIdAsync(request.InventoryId);
        findInventory.ItemId = request.ItemId;
        findInventory.Cost = request.Cost;
        findInventory.SalePrice = request.SalePrice;
        findInventory.CurrentQuantity = request.CurrentQuantity;
        findInventory.CriticalQuantity = request.CriticalQuantity;

        var result = await _unitOfWork.CompleteAsync() > 0;

        if (!result) return Result<Guid>.Failure("Failed to update inventory.");

        return Result<Guid>.Success(findInventory.InventoryId);
    }
}
