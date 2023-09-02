using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PracticeInventory.Application.Role.Commands;
using PracticeInventory.Application.Role.Queries;
using PracticeInventory.Domain.Enums;

namespace PracticeInventory.API.Controllers;

[Authorize(Roles = DefaultUserRole.Admin)]
public class RoleController : BaseController
{
    [AllowAnonymous]
    [HttpGet("roles")]
    public async Task<IActionResult> Roles()
    {
        return HandleResult(await Mediator.Send(new GetRolesQuery()));
    }

    [HttpPost("add-role")]
    public async Task<IActionResult> AddRole(AddRoleCommand command)
    {
        return HandleResult(await Mediator.Send(command));
    }

    [HttpPut("update-role")]
    public async Task<IActionResult> UpdateRole(UpdateRoleCommand command)
    {
        return HandleResult(await Mediator.Send(command));
    }

    [HttpDelete("delete-role/{RoleId}")]
    public async Task<IActionResult> DeleteRole(string RoleId)
    {
        return HandleResult(await Mediator.Send(new DeleteRoleCommand(){ RoleId = RoleId}));
    }
}