using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using PracticeInventory.Domain.Enums;

namespace PracticeInventory.Persistence.Seeds;

public class SeedUsers
{
    public static async Task SeedData(IServiceProvider serviceProvider)
    {
        var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

        var defaultEmail = DefaultUsers.DefaultEmail;

        var user = new IdentityUser
        {
            Email = defaultEmail,
            NormalizedEmail = defaultEmail.ToUpper(),
            UserName = defaultEmail,
            NormalizedUserName = defaultEmail.ToUpper(),
            PhoneNumber = DefaultUsers.DefaultPhoneNo,
            EmailConfirmed = true,
            PhoneNumberConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString()
        };

        if (!context.Users.Any(u => u.UserName == user.UserName))
        {
            var password = new PasswordHasher<IdentityUser>();
            user.PasswordHash = password.HashPassword(user, DefaultUsers.DefaultPassword);

            var userStore = new UserStore<IdentityUser>(context);
            var resultCreate = await userStore?.CreateAsync(user);

            var _userManager = serviceProvider.GetService<UserManager<IdentityUser>>();
            var resultAssign = await _userManager?.AddToRoleAsync(user, DefaultUserRole.Admin);
        }
        await context.SaveChangesAsync();
    }
}
