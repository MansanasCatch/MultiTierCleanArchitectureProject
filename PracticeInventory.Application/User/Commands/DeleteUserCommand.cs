using MediatR;
using PracticeInventory.Core.Results;
using System.ComponentModel.DataAnnotations;

namespace PracticeInventory.Application.User.Commands;

public class DeleteUserCommand : IRequest<Result<Unit>>
{
    [Required(ErrorMessage = "UserId is required")]
    public string UserId { get; set; }
}