using FluentValidation;
using PracticeInventory.Application.Role.Commands;

namespace PracticeInventory.Application.Role.Validator;

public class AddRoleCommandValidator : AbstractValidator<AddRoleCommand>
{
    public AddRoleCommandValidator()
    {
        RuleFor(x => x.RoleName)
            .NotNull()
            .NotEmpty()
            .MinimumLength(3);
    }
}
