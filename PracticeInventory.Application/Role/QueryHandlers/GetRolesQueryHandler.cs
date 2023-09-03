using MediatR;
using Microsoft.AspNetCore.Identity;
using PracticeInventory.Application.Role.DTO;
using PracticeInventory.Application.Role.Queries;
using PracticeInventory.Core.Results;

namespace PracticeInventory.Application.Role.QueryHandlers;

public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, Result<IEnumerable<RoleDTO>>>
{
    private readonly RoleManager<IdentityRole> _roleManager;
    public GetRolesQueryHandler(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<Result<IEnumerable<RoleDTO>>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
    {
        //THIS IS NOT A BEST PRACTICE THIS IS A SAMPLE OF MANUAL MAPPING
        //USE DAPPER IMPLEMENTATION like in GetInventoriesQueryHandler
        var roles = _roleManager.Roles.ToList();
        var mappedQuery = RoleDTO.ToRoleDTOMappedList(roles);
        return Result<IEnumerable<RoleDTO>>.Success(mappedQuery);
    }
}