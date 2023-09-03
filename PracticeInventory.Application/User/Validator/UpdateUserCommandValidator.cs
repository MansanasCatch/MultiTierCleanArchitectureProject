using FluentValidation;
using PracticeInventory.Application.User.Commands;

namespace PracticeInventory.Application.User.Validator;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(x => x.UserName).MinimumLength(5);
        RuleFor(x => x.Email).EmailAddress();
        RuleFor(x => x.PhoneNumber).MinimumLength(11);
    }
}
