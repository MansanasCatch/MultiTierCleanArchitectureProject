using MediatR;
using Microsoft.AspNetCore.Mvc;
using PracticeInventory.Core.Results;

namespace PracticeInventory.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BaseController : ControllerBase
{
    private IMediator _mediator;
    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    protected ActionResult HandleResult<T>(Result<T> result)
    {
        if (result.IsSuccess)
            return Ok(result.Value);
        return BadRequest(result.Error);
    }
}