using MediatR;
using Microsoft.AspNetCore.Identity;
using PracticeInventory.Application.User.Commands;
using PracticeInventory.Core.Results;
using PracticeInventory.Domain.Enums;

namespace PracticeInventory.Application.User.CommandHandlers;

internal class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Result<Unit>>
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    public UpdateUserCommandHandler(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<Result<Unit>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var findUser = await _userManager.FindByIdAsync(request.UserId);
        if (findUser is null)
        {
            return Result<Unit>.Failure("User is not exist.");
        }

        if (findUser.Email == DefaultUsers.DefaultEmail)
        {
            return Result<Unit>.Failure("Cannot update default users.");
        }

        findUser.Email= request.Email;
        findUser.NormalizedEmail = request.Email.ToUpper();
        findUser.UserName= request.UserName;
        findUser.NormalizedUserName = request.UserName.ToUpper();
        findUser.PhoneNumber = request.PhoneNumber;

        var resultUpdate = await _userManager.UpdateAsync(findUser);
        if (resultUpdate.Succeeded)
        {
            return Result<Unit>.Success(Unit.Value);
        }
        return Result<Unit>.Failure("Failed to update user.");
    }
}