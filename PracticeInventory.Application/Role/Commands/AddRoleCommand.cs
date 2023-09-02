using MediatR;
using PracticeInventory.Core.Results;
using System.ComponentModel.DataAnnotations;

namespace PracticeInventory.Application.Role.Commands;

public class AddRoleCommand : IRequest<Result<Unit>>
{
    [Required(ErrorMessage = "RoleName is required")]
    public string RoleName { get; set; }
}