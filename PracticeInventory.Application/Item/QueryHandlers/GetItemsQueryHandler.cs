using MediatR;
using PracticeInventory.Application.Item.DTO;
using PracticeInventory.Application.Item.Queries;
using PracticeInventory.Core.Results;
using PracticeInventory.Domain.Interfaces;

namespace PracticeInventory.Application.Item.QueryHandlers;

public class GetItemsQueryHandler : IRequestHandler<GetItemsQuery, Result<IEnumerable<ItemDTO>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetItemsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<IEnumerable<ItemDTO>>> Handle(GetItemsQuery request, CancellationToken cancellationToken)
    {
        var items = _unitOfWork.ItemRepository.GetItems().Result.ToList();
        foreach (var item in items)
        {
            item.Category = _unitOfWork.CategoryRepository.FindQueryable(x => x.CategoryId == item.CategoryId).FirstOrDefault();
        }
        var mappedQuery = ItemDTO.ToItemDTOMappedList(items);

        return Result<IEnumerable<ItemDTO>>.Success(mappedQuery);
    }
}