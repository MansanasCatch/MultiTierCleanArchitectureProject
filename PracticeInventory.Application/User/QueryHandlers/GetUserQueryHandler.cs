using Dapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PracticeInventory.Application.User.DTO;
using PracticeInventory.Application.User.Queries;
using PracticeInventory.Core.Results;
using PracticeInventory.Domain.Interfaces;

namespace PracticeInventory.Application.User.QueryHandlers;

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, Result<UserDTO>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;
    public GetUserQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<UserDTO>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.Create();
        const string sql = @"SELECT users.Id, 
                             users.UserName,
                             users.Email,
                             users.PhoneNumber, 
                             roles.Id  as RoleId,
		                     roles.Name  as RoleName
		                     FROM AspNetUsers AS users
                             LEFT JOIN AspNetUserRoles AS userRoles ON users.Id = userRoles.UserId
                             LEFT JOIN AspNetRoles AS roles ON userRoles.RoleId = roles.Id
							 WHERE users.Id = @UserId";
        var users = await connection.QueryFirstOrDefaultAsync<UserDTO>(sql, new { request.UserId });
        return Result<UserDTO>.Success(users);
    }
}