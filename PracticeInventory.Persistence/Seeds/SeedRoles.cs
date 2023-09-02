using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using PracticeInventory.Domain.Enums;

namespace PracticeInventory.Persistence.Seeds;

public class SeedRoles
{
    public static async Task SeedData(IServiceProvider serviceProvider)
    {
        var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

        string[] roles = new string[] { DefaultUserRole.Admin, DefaultUserRole.RegularUser };
        foreach (string role in roles)
        {
            var roleStore = new RoleStore<IdentityRole>(context);

            if (!context.Roles.Any(r => r.Name == role))
            {
                var roleModel = new IdentityRole();
                roleModel.Name = role;
                roleModel.NormalizedName = role.ToUpper();
                await roleStore.CreateAsync(roleModel);
            }
        }
        context?.SaveChangesAsync();
    }
}
