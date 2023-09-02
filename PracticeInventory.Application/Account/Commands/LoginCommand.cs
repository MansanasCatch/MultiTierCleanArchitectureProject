using MediatR;
using PracticeInventory.Core.Results;
using System.ComponentModel.DataAnnotations;

namespace PracticeInventory.Application.Account.Commands;

public class LoginCommand : IRequest<Result<string>>
{
    [Required(ErrorMessage = "User Name is required")]
    public string Username { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; } = string.Empty;
}