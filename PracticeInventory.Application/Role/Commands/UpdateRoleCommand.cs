using MediatR;
using PracticeInventory.Core.Results;
using System.ComponentModel.DataAnnotations;

namespace PracticeInventory.Application.Role.Commands;

public class UpdateRoleCommand : IRequest<Result<Unit>>
{
    [Required(ErrorMessage = "RoleId is required")]
    public string RoleId { get; set; }
    [Required(ErrorMessage = "RoleName is required")]
    public string RoleName { get; set; }
}