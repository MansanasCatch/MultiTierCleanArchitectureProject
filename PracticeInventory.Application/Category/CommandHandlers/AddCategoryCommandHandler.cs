using MediatR;
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
        var category = new Categories()
        {
            CategoryId = Guid.NewGuid(),
            CategoryName = request.CategoryName
        };
        _unitOfWork.CategoryRepository.Add(category);

        var result = await _unitOfWork.CompleteAsync() > 0;

        if (!result) return Result<Guid>.Failure("Failed to add category.");

        return Result<Guid>.Success(category.CategoryId);
    }
}