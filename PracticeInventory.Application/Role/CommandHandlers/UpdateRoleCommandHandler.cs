using MediatR;
using Microsoft.AspNetCore.Identity;
using PracticeInventory.Application.Role.Commands;
using PracticeInventory.Core.Results;
using PracticeInventory.Domain.Enums;

namespace PracticeInventory.Application.Role.CommandHandlers;

public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, Result<Unit>>
{
    private readonly RoleManager<IdentityRole> _roleManager;
    public UpdateRoleCommandHandler(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<Result<Unit>> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        var findRole = await _roleManager.FindByIdAsync(request.RoleId);

        if(findRole is null)
        {
            return Result<Unit>.Failure("Role is not exist.");
        }

        if (findRole.Name == DefaultUserRole.Admin || findRole.Name == DefaultUserRole.RegularUser)
        {
            return Result<Unit>.Failure("Cannot update default roles.");
        }

        findRole.Name = request.RoleName;
        findRole.NormalizedName = request.RoleName.ToUpper();

        await _roleManager.UpdateAsync(findRole);

        return Result<Unit>.Success(Unit.Value);
    }
}