using System.ComponentModel.DataAnnotations;

namespace PracticeInventory.WebUI.Models.DTO.Account;

public class LoginDTO
{
    [Required(ErrorMessage = "User Name is required")]
    public string Username { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; } = string.Empty;
}
