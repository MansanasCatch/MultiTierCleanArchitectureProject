using MediatR;
using Microsoft.AspNetCore.Identity;
using PracticeInventory.Application.Role.Commands;
using PracticeInventory.Core.Results;
using PracticeInventory.Domain.Enums;

namespace PracticeInventory.Application.Role.CommandHandlers;

public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, Result<Unit>>
{
    private readonly RoleManager<IdentityRole> _roleManager;
    public DeleteRoleCommandHandler(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<Result<Unit>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        var findRole = await _roleManager.FindByIdAsync(request.RoleId);

        if (findRole is null)
        {
            return Result<Unit>.Failure("Role is not exist.");
        }

        if (findRole.Name == DefaultUserRole.Admin || findRole.Name == DefaultUserRole.RegularUser)
        {
            return Result<Unit>.Failure("Cannot delete default roles.");
        }

        await _roleManager.DeleteAsync(findRole);

        return Result<Unit>.Success(Unit.Value);
    }
}