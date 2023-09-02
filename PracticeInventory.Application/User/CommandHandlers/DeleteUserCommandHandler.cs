using MediatR;
using Microsoft.AspNetCore.Identity;
using PracticeInventory.Application.User.Commands;
using PracticeInventory.Core.Results;
using PracticeInventory.Domain.Enums;

namespace PracticeInventory.Application.User.CommandHandlers;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Result<Unit>>
{
    private readonly UserManager<IdentityUser> _userManager;
    public DeleteUserCommandHandler(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Result<Unit>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var findUser = await _userManager.FindByIdAsync(request.UserId);

        if (findUser is null)
        {
            return Result<Unit>.Failure("User is not exist.");
        }

        if (findUser.Email == DefaultUsers.DefaultEmail)
        {
            return Result<Unit>.Failure("Cannot delete default users.");
        }

        await _userManager.DeleteAsync(findUser);

        return Result<Unit>.Success(Unit.Value);
    }
}