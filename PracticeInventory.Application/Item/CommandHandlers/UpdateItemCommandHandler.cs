using MediatR;
using PracticeInventory.Application.Item.Commands;
using PracticeInventory.Core.Results;
using PracticeInventory.Domain.Entities;
using PracticeInventory.Domain.Interfaces;

namespace PracticeInventory.Application.Item.CommandHandlers;

public class UpdateItemCommandHandler : IRequestHandler<UpdateItemCommand, Result<Guid>>
{
    private readonly IUnitOfWork _unitOfWork;
    public UpdateItemCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<Guid>> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
    {
        var findItem = await _unitOfWork.ItemRepository.GetByIdAsync(request.ItemId);

        findItem.CategoryId = request.CategoryId;
        findItem.ItemName = request.ItemName;

        var result = await _unitOfWork.CompleteAsync() > 0;

        if (!result) return Result<Guid>.Failure("Failed to update item.");

        return Result<Guid>.Success(findItem.ItemId);
    }
}
