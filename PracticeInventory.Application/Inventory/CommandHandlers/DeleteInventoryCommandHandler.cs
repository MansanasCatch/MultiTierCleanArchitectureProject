using MediatR;
using Microsoft.EntityFrameworkCore;
using PracticeInventory.Application.Inventory.Commands;
using PracticeInventory.Core.Results;
using PracticeInventory.Domain.Interfaces;

namespace PracticeInventory.Application.Inventory.CommandHandlers;

public class DeleteInventoryCommandHandler : IRequestHandler<DeleteInventoryCommand, Result<Unit>>
{
    private readonly IUnitOfWork _unitOfWork;
    public DeleteInventoryCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<Unit>> Handle(DeleteInventoryCommand request, CancellationToken cancellationToken)
    {
        await _unitOfWork.InventoryRepository.FindQueryable(x => x.InventoryId == request.InventoryId).ExecuteDeleteAsync();

        return Result<Unit>.Success(Unit.Value);
    }
}