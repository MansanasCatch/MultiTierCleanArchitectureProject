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
        var isItemExist = _unitOfWork.ItemRepository.FindQueryable(x => x.ItemName == request.ItemName);
        if (isItemExist is not null)
        {
            return Result<Guid>.Failure("Item Name is already exist.");
        }

        var findItem = await _unitOfWork.ItemRepository.GetByIdAsync(request.ItemId);
        if (findItem is null)
        {
            return Result<Guid>.Failure("ItemId is not exist.");
        }

        findItem.CategoryId = request.CategoryId;
        findItem.ItemName = request.ItemName;

        await _unitOfWork.CompleteAsync();

        return Result<Guid>.Success(findItem.ItemId);
    }
}
