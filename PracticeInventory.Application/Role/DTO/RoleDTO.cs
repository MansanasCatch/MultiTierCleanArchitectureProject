using Microsoft.AspNetCore.Identity;
using PracticeInventory.Application.Category.DTO;

namespace PracticeInventory.Application.Role.DTO;

public class RoleDTO
{
    public string Id { get; set; }
    public string Name { get; set; }
    public static IEnumerable<RoleDTO> ToRoleDTOMappedList(IEnumerable<IdentityRole> source)
    {
        if (source is null)
        {
            throw new InvalidOperationException("Cannot mapped Role to DTO");
        }

        return source.Select(item => new RoleDTO
        {
            Id = item.Id,
            Name = item.Name
        });
    }
}
