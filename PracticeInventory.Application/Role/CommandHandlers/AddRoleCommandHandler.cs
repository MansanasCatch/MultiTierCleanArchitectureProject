using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using PracticeInventory.Application.Category.Commands;
using PracticeInventory.Application.Role.Commands;
using PracticeInventory.Core.Results;
using PracticeInventory.Domain.Interfaces;
using System.Data;

namespace PracticeInventory.Application.Role.CommandHandlers;

public class AddRoleCommandHandler : IRequestHandler<AddRoleCommand, Result<Unit>>
{
    private readonly RoleManager<IdentityRole> _roleManager;
    public AddRoleCommandHandler(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<Result<Unit>> Handle(AddRoleCommand request, CancellationToken cancellationToken)
    {
        var isRoleExist = await _roleManager.RoleExistsAsync(request.RoleName);
        if (!isRoleExist)
        {
            var role = new IdentityRole();
            role.Name = request.RoleName;
            role.NormalizedName = request.RoleName.ToUpper();

            await _roleManager.CreateAsync(role);

            return Result<Unit>.Success(Unit.Value);
        }

        return Result<Unit>.Failure("Role is already exist.");
    }
}