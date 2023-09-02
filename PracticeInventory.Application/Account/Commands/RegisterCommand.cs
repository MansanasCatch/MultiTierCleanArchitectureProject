using MediatR;
using PracticeInventory.Core.Results;
using System.ComponentModel.DataAnnotations;

namespace PracticeInventory.Application.Account.Commands;

public class RegisterCommand : IRequest<Result<string>>
{
    [Required(ErrorMessage = "RoleId is required")]
    public string RoleId { get; set; } = string.Empty;
    [Required(ErrorMessage = "User Name is required")]
    public string Username { get; set; } = string.Empty;

    [EmailAddress]
    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; } = string.Empty;
    [Required(ErrorMessage = "PhoneNumber is required")]
    public string PhoneNumber { get; set; } = string.Empty;
}