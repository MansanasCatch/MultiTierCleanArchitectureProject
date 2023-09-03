using FluentValidation;
using PracticeInventory.Application.User.Commands;

namespace PracticeInventory.Application.User.Validator;

public class AddUserCommandValidator : AbstractValidator<AddUserCommand>
{
    public AddUserCommandValidator()
    {
        RuleFor(x => x.RoleId).NotNull();
        RuleFor(x => x.UserName).MinimumLength(5);
        RuleFor(x => x.Email).EmailAddress();
        RuleFor(x => x.Password).MinimumLength(8);
        RuleFor(x => x.PhoneNumber).MinimumLength(11);
    }
}
