using MediatR;
using Microsoft.AspNetCore.Identity;
using PracticeInventory.Application.Account.Commands;
using PracticeInventory.Core.Results;
using PracticeInventory.Domain.Interfaces;

namespace PracticeInventory.Application.Account.CommandHandlers;

public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<string>>
{
    private readonly IJwtProvider _jwtProvider;
    private readonly UserManager<IdentityUser> _userManager;

    public LoginCommandHandler(UserManager<IdentityUser> userManager, IJwtProvider jwtProvider)
    {
        _userManager = userManager;
        _jwtProvider = jwtProvider;
    }
    public async Task<Result<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.Username);
        if (user is null)
        {
            return Result<string>.Failure("User is not exist.");
        }

        if (await _userManager.CheckPasswordAsync(user, request.Password))
        {
            var token = await _jwtProvider.Generate(user);
            return Result<string>.Success(token);
        }

        return Result<string>.Failure("Wrong Passwor");
    }
}