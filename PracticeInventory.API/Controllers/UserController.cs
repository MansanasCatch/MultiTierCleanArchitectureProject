using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PracticeInventory.Application.Role.Commands;
using PracticeInventory.Application.User.Commands;
using PracticeInventory.Application.User.Queries;
using PracticeInventory.Domain.Enums;

namespace PracticeInventory.API.Controllers;

[Authorize(Roles = DefaultUserRole.Admin)]
public class UserController : BaseController
{
    [HttpGet("users")]
    public async Task<IActionResult> Users()
    {
        return HandleResult(await Mediator.Send(new GetUsersQuery()));
    }

    [HttpGet("detail-user/{UserId}")]
    public async Task<IActionResult> DetailUser(string UserId)
    {
        return HandleResult(await Mediator.Send(new GetUserQuery() { UserId = UserId}));
    }

    [HttpPost("add-user")]
    public async Task<IActionResult> AddUser(AddUserCommand command)
    {
        return HandleResult(await Mediator.Send(command));
    }

    [HttpPut("update-user")]
    public async Task<IActionResult> UpdateUser(UpdateUserCommand command)
    {
        return HandleResult(await Mediator.Send(command));
    }

    [HttpDelete("delete-user/{UserId}")]
    public async Task<IActionResult> DeleteUser(string UserId)
    {
        return HandleResult(await Mediator.Send(new DeleteUserCommand() { UserId = UserId }));
    }
}