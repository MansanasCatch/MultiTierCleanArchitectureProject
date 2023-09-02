
using MediatR;
using Microsoft.AspNetCore.Identity;
using PracticeInventory.Application.Account.Commands;
using PracticeInventory.Core.Results;
using PracticeInventory.Domain.Enums;

namespace PracticeInventory.Application.Account.CommandHandlers;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result<string>>
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public RegisterCommandHandler(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<Result<string>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var isUserExist = _userManager.Users.Any(u => u.UserName == request.Username);
        if (isUserExist)
        {
            return Result<string>.Failure("User is already exist.");
        }

        var findRole = await _roleManager.FindByIdAsync(request.RoleId);
        if (findRole is null)
        {
            return Result<string>.Failure("Role is not exist.");
        }

        var user = new IdentityUser
        {
            Email = request.Email,
            NormalizedEmail = request.Email.ToUpper(),
            UserName = request.Username,
            NormalizedUserName = request.Username.ToUpper(),
            PhoneNumber = request.PhoneNumber,
            EmailConfirmed = true,
            PhoneNumberConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString()
        };

        var passwordHasher = new PasswordHasher<IdentityUser>();
        user.PasswordHash = passwordHasher.HashPassword(user, request.Password);

        var resultCreate = await _userManager.CreateAsync(user);
        if (resultCreate.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, findRole.Name);
        }

        return Result<string>.Success(user.Id);
    }
}