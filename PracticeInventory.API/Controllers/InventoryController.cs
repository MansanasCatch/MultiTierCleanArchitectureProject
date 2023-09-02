using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PracticeInventory.Application.Inventory.Commands;
using PracticeInventory.Application.Inventory.Queries;
using PracticeInventory.Domain.Enums;

namespace PracticeInventory.API.Controllers;

public class InventoryController : BaseController
{
    [Authorize(Policy = "InventoryPolicy")]
    [HttpGet("inventories")]
    public async Task<IActionResult> Inventories()
    {
        return HandleResult(await Mediator.Send(new GetInventoriesQuery()));
    }

    [Authorize(Roles = DefaultUserRole.RegularUser)]

    [HttpPost("add-inventory")]
    public async Task<IActionResult> AddInventory(AddInventoryCommand command)
    {
        return HandleResult(await Mediator.Send(command));
    }

    [Authorize(Roles = DefaultUserRole.RegularUser)]

    [HttpPut("update-inventory")]
    public async Task<IActionResult> UpdateInventory(UpdateInventoryCommand command)
    {
        return HandleResult(await Mediator.Send(command));
    }

    [Authorize(Roles = DefaultUserRole.RegularUser)]

    [HttpDelete("delete-inventory/{InventoryId}")]
    public async Task<IActionResult> DeleteInventory(Guid InventoryId)
    {
        return HandleResult(await Mediator.Send(new DeleteInventoryCommand() { InventoryId = InventoryId }));
    }
}