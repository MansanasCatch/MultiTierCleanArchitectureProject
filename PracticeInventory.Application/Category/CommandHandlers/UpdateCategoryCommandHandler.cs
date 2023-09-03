using MediatR;
using PracticeInventory.Application.Category.Commands;
using PracticeInventory.Core.Results;
using PracticeInventory.Domain.Interfaces;

namespace PracticeInventory.Application.Category.CommandHandlers;

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Result<Guid>>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCategoryCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var isCategoryExist = _unitOfWork.CategoryRepository.FindQueryable(x => x.CategoryName == request.CategoryName).FirstOrDefault();
        if (isCategoryExist is not null)
        {
            return Result<Guid>.Failure("Category Name is already exist.");
        }

        var findCategory = await _unitOfWork.CategoryRepository.GetByIdAsync(request.CategoryId);
        if (findCategory is null)
        {
            return Result<Guid>.Failure("CategoryId is not exist.");
        }

        findCategory.CategoryName = request.CategoryName;

        await _unitOfWork.CompleteAsync();

        return Result<Guid>.Success(findCategory.CategoryId);
    }
}