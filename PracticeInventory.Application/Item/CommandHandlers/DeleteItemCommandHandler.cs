using MediatR;
using Microsoft.EntityFrameworkCore;
using PracticeInventory.Application.Item.Commands;
using PracticeInventory.Core.Results;
using PracticeInventory.Domain.Interfaces;

namespace PracticeInventory.Application.Item.CommandHandlers;

public class DeleteItemCommandHandler : IRequestHandler<DeleteItemCommand, Result<Unit>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteItemCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Unit>> Handle(DeleteItemCommand request, CancellationToken cancellationToken)
    {
        var isItemExist = _unitOfWork.ItemRepository.FindQueryable(x => x.ItemId == request.ItemId);
        if (isItemExist is null)
        {
            return Result<Unit>.Failure("ItemId is not exist.");
        }

        await isItemExist.ExecuteDeleteAsync();

        return Result<Unit>.Success(Unit.Value);
    }
}