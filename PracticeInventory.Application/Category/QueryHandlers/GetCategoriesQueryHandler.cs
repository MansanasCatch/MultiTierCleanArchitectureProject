using Dapper;
using MediatR;
using PracticeInventory.Application.Category.DTO;
using PracticeInventory.Application.Category.Queries;
using PracticeInventory.Domain.Entities;
using PracticeInventory.Domain.Interfaces;

namespace PracticeInventory.Application.Category.QueryHandlers;

public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, IEnumerable<CategoryDTO>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetCategoriesQueryHandler(IUnitOfWork unitOfWork, ISqlConnectionFactory sqlConnectionFactory)
    {
        _unitOfWork = unitOfWork;
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<IEnumerable<CategoryDTO>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        //IEnumerable<CategoryDTO> categories = new List<CategoryDTO>()
        //{
        //    new CategoryDTO()
        //    {
        //        CategoryId = Guid.NewGuid(),
        //        CategoryName = "Sample Category"
        //    }
        //};

        //var categories = await _unitOfWork.CategoryRepository.GetCategories();

        using var connection = _sqlConnectionFactory.Create();
        const string sql = "SELECT CategoryId, CategoryName FROM Categories";
        var categories = await connection.QueryAsync<Categories>(sql);

        var mappedQuery = CategoryDTO.ToCategoryDTOMappedList(categories);
       
        return mappedQuery;
    }
}