using MediatR;
using PracticeInventory.Application.User.DTO;
using PracticeInventory.Core.Results;

namespace PracticeInventory.Application.User.Queries;

public class GetUserQuery : IRequest<Result<UserDTO>>
{
    public string UserId { get; set; }
}