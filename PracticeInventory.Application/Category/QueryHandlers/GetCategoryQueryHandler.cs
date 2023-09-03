using MediatR;
using PracticeInventory.Application.Category.DTO;
using PracticeInventory.Application.Category.Queries;
using PracticeInventory.Core.Results;
using PracticeInventory.Domain.Interfaces;

namespace PracticeInventory.Application.Category.QueryHandlers;

public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, Result<CategoryDTO>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetCategoryQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<CategoryDTO>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
    {
        //THIS IS NOT A BEST PRACTICE THIS IS A SAMPLE OF MANUAL MAPPING
        //USE DAPPER IMPLEMENTATION like in GetInventoriesQueryHandler
        var category = await _unitOfWork.CategoryRepository.GetByIdAsync(request.CategoryId);
        var mappedQuery = CategoryDTO.ToCategoryDTOMapped(category);

        return Result<CategoryDTO>.Success(mappedQuery);
    }
}