﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using PracticeInventory.Application.Category.Commands;
using PracticeInventory.Core.Results;
using PracticeInventory.Domain.Interfaces;

namespace PracticeInventory.Application.Category.CommandHandlers;

public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Result<Unit>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCategoryCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Unit>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var isCategoryExist = _unitOfWork.CategoryRepository.FindQueryable(x => x.CategoryId == request.CategoryId);
        if (isCategoryExist is null)
        {
            return Result<Unit>.Failure("CategoryId is not exist.");
        }

        await isCategoryExist.ExecuteDeleteAsync();

        return Result<Unit>.Success(Unit.Value);
    }
}