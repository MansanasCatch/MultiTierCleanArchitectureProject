using MediatR;
using Microsoft.AspNetCore.Identity;
using PracticeInventory.Application.Category.Commands;
using PracticeInventory.Core.Results;
using PracticeInventory.Domain.Entities;
using PracticeInventory.Domain.Interfaces;

namespace PracticeInventory.Application.Category.CommandHandlers;

public class AddCategoryCommandHandler : IRequestHandler<AddCategoryCommand, Result<Guid>>
{
    private readonly IUnitOfWork _unitOfWork;

    public AddCategoryCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
    {
        var isCategoryExist = _unitOfWork.CategoryRepository.FindQueryable(x => x.CategoryName == request.CategoryName).FirstOrDefault();
        if (isCategoryExist is not null)
        {
            return Result<Guid>.Failure("Category Name is already exist.");
        }

        var category = new Categories()
        {
            CategoryId = Guid.NewGuid(),
            CategoryName = request.CategoryName
        };
        _unitOfWork.CategoryRepository.Add(category);

        await _unitOfWork.CompleteAsync();

        return Result<Guid>.Success(category.CategoryId);
    }
}