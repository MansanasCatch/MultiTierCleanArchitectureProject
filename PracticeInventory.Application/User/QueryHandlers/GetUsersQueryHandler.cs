using Dapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PracticeInventory.Application.User.DTO;
using PracticeInventory.Application.User.Queries;
using PracticeInventory.Core.Results;
using PracticeInventory.Domain.Interfaces;

namespace PracticeInventory.Application.User.QueryHandlers;

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, Result<IEnumerable<UserDTO>>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;
    private readonly ILogger _logger;
    public GetUsersQueryHandler(ILogger<GetUsersQueryHandler> logger, ISqlConnectionFactory sqlConnectionFactory)
    {
        _logger = logger;
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<IEnumerable<UserDTO>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        //_logger.LogInformation("Starting to do slow work");
        using var connection = _sqlConnectionFactory.Create();
        const string sql = @"SELECT users.Id, 
                             users.UserName,
                             users.Email,
                             users.PhoneNumber, 
                             roles.Id  as RoleId,
		                     roles.Name  as RoleName
		                     FROM AspNetUsers AS users
                             LEFT JOIN AspNetUserRoles AS userRoles ON users.Id = userRoles.UserId
                             LEFT JOIN AspNetRoles AS roles ON userRoles.RoleId = roles.Id";
        var users = await connection.QueryAsync<UserDTO>(sql);
        return Result<IEnumerable<UserDTO>>.Success(users);
    }
}