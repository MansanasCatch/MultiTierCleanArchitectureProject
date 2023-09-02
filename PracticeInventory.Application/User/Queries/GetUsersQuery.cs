using MediatR;
using PracticeInventory.Application.User.DTO;
using PracticeInventory.Core.Results;

namespace PracticeInventory.Application.User.Queries;

public class GetUsersQuery : IRequest<Result<IEnumerable<UserDTO>>>
{
}
