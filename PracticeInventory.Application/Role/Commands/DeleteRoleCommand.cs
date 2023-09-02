using MediatR;
using PracticeInventory.Core.Results;
using System.ComponentModel.DataAnnotations;

namespace PracticeInventory.Application.Role.Commands;

public class DeleteRoleCommand : IRequest<Result<Unit>>
{
    [Required(ErrorMessage = "RoleId is required")]
    public string RoleId { get; set; }
}