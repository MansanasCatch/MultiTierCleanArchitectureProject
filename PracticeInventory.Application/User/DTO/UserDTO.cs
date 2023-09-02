using Microsoft.AspNetCore.Identity;

namespace PracticeInventory.Application.User.DTO;

public class UserDTO
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string RoleId { get; set; }
    public string RoleName { get; set; }
}
