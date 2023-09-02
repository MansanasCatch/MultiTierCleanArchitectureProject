using System.ComponentModel.DataAnnotations;

namespace PracticeInventory.WebUI.Models.DTO.User;

public class AddUserDTO
{
    [Required(ErrorMessage = "RoleId is required")]
    public string RoleId { get; set; } = string.Empty;
    [EmailAddress]
    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; }
    [Required(ErrorMessage = "UserName is required")]
    public string UserName { get; set; }
    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }
    [Required(ErrorMessage = "PhoneNumber is required")]
    public string PhoneNumber { get; set; }
}
