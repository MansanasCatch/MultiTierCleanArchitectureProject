using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PracticeInventory.Application.Category.Commands;
using PracticeInventory.Application.Item.Commands;
using PracticeInventory.Application.Item.Queries;
using PracticeInventory.Domain.Enums;

namespace PracticeInventory.API.Controllers;

[Authorize(Roles = DefaultUserRole.RegularUser)]
public class ItemController : BaseController
{
    [HttpGet("items")]
    public async Task<IActionResult> Items()
    {
        return HandleResult(await Mediator.Send(new GetItemsQuery()));
    }

    [HttpPost("add-item")]
    public async Task<IActionResult> AddItem(AddItemCommand command)
    {
        return HandleResult(await Mediator.Send(command));
    }

    [HttpPut("update-item")]
    public async Task<IActionResult> UpdateItem(UpdateItemCommand command)
    {
        return HandleResult(await Mediator.Send(command));
    }

    [HttpDelete("delete-item/{ItemId}")]
    public async Task<IActionResult> DeleteItem(Guid ItemId)
    {
        return HandleResult(await Mediator.Send(new DeleteItemCommand() { ItemId = ItemId }));
    }
}