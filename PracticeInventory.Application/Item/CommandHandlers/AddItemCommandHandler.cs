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
        var isItemExist = _unitOfWork.ItemRepository.FindQueryable(x => x.ItemName == request.ItemName).FirstOrDefault();
        if (isItemExist is not null)
        {
            return Result<Guid>.Failure("Item Name is already exist.");
        }

        var item = new Items()
        {
            ItemId = Guid.NewGuid(),
            CategoryId = request.CategoryId,
            ItemName = request.ItemName
        };
        _unitOfWork.ItemRepository.Add(item);

        await _unitOfWork.CompleteAsync();

        return Result<Guid>.Success(item.ItemId);
    }
}
