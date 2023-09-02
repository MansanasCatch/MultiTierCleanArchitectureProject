using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PracticeInventory.Application.Category.Commands;
using PracticeInventory.Application.Category.Queries;
using PracticeInventory.Domain.Enums;
using System.Data;

namespace PracticeInventory.API.Controllers;

[Authorize(Roles = DefaultUserRole.RegularUser)]
public class CategoryController : BaseController
{
    [HttpGet("categories")]
    public async Task<IActionResult> Categories()
    {
        var categories = await Mediator.Send(new GetCategoriesQuery());
        if (categories.Any())
        {
            return Ok(categories);
        }
        return NoContent();
    }

    [HttpGet("category/{CategoryId}")]
    public async Task<IActionResult> Category(Guid CategoryId)
    {

        return HandleResult(await Mediator.Send(new GetCategoryQuery() { CategoryId = CategoryId }));
    }

    [HttpPost("add-category")]
    public async Task<IActionResult> AddCategory(AddCategoryCommand command)
    {
        return HandleResult(await Mediator.Send(command));
    }

    [HttpPut("update-category")]
    public async Task<IActionResult> UpdateCategory(UpdateCategoryCommand command)
    {
        return HandleResult(await Mediator.Send(command));
    }

    [HttpDelete("delete-category/{CategoryId}")]
    public async Task<IActionResult> DeleteCategory(Guid CategoryId)
    {
        return HandleResult(await Mediator.Send(new DeleteCategoryCommand() { CategoryId = CategoryId }));
    }
}