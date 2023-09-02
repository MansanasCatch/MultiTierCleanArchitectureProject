using MediatR;
using PracticeInventory.Application.Role.DTO;
using PracticeInventory.Core.Results;

namespace PracticeInventory.Application.Role.Queries;

public class GetRolesQuery : IRequest<Result<IEnumerable<RoleDTO>>>
{
}
