using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PracticeInventory.Application.Account.Commands;

namespace PracticeInventory.API.Controllers;

[AllowAnonymous]
public class AccountController : BaseController
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand command)
    {
        return HandleResult(await Mediator.Send(command));
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterCommand command)
    {
        return HandleResult(await Mediator.Send(command));
    }
}