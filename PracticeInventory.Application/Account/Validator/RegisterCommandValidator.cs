using FluentValidation;
using PracticeInventory.Application.Account.Commands;
using System;

namespace PracticeInventory.Application.Account.Validator;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.RoleId).NotNull();
        RuleFor(x => x.Username).MinimumLength(5);
        RuleFor(x => x.Email).EmailAddress();
        RuleFor(x => x.Password).MinimumLength(8);
        RuleFor(x => x.PhoneNumber).MinimumLength(11);
    }
}
