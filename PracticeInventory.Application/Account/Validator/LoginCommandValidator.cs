using FluentValidation;
using PracticeInventory.Application.Account.Commands;

namespace PracticeInventory.Application.Account.Validator;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.Username)
            .NotNull()
            .NotEmpty()
            .MinimumLength(5);
        RuleFor(x => x.Password)
            .NotNull()
            .NotEmpty()
            .MinimumLength(8);
    }
}
