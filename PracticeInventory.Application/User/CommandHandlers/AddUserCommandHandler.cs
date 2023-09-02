using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PracticeInventory.Application.User.Commands;
using PracticeInventory.Core.Results;
using PracticeInventory.Domain.Enums;
using System;

namespace PracticeInventory.Application.User.CommandHandlers;

public class AddUserCommandHandler : IRequestHandler<AddUserCommand, Result<Unit>>
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    public AddUserCommandHandler(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<Result<Unit>> Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        var isUserExist = _userManager.Users.Any(u => u.UserName == request.UserName);
        if (isUserExist)
        {
            return Result<Unit>.Failure("User is already exist.");
        }

        var findRole = await _roleManager.FindByIdAsync(request.RoleId);
        if (findRole is null)
        {
            return Result<Unit>.Failure("Role is not exist.");
        }

        var user = new IdentityUser
        {
            Email = request.Email,
            NormalizedEmail = request.Email.ToUpper(),
            UserName = request.UserName,
            NormalizedUserName = request.UserName.ToUpper(),
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

        return Result<Unit>.Success(Unit.Value);
    }
}