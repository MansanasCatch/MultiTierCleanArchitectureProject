using MediatR;
using PracticeInventory.Application.Item.Commands;
using PracticeInventory.Core.Results;
using PracticeInventory.Domain.Entities;
using PracticeInventory.Domain.Interfaces;

namespace PracticeInventory.Application.Item.CommandHandlers;

public class AddItemCommandHandler : IRequestHandler<AddItemCommand, Result<Guid>>
{
    private readonly IUnitOfWork _unitOfWork;
    public AddItemCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<Guid>> Handle(AddItemCommand request, CancellationToken cancellationToken)
    {
        var item = new Items()
        {
            ItemId = Guid.NewGuid(),
            CategoryId = request.CategoryId,
            ItemName = request.ItemName
        };
        _unitOfWork.ItemRepository.Add(item);

        var result = await _unitOfWork.CompleteAsync() > 0;

        if (!result) return Result<Guid>.Failure("Failed to add item.");

        return Result<Guid>.Success(item.ItemId);
    }
}
