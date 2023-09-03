using FluentValidation;
using PracticeInventory.Application.Role.Commands;

namespace PracticeInventory.Application.Role.Validator;

public class UpdateRoleCommandValidator : AbstractValidator<UpdateRoleCommand>
{
    public UpdateRoleCommandValidator()
    {
        RuleFor(x => x.RoleName)
            .NotNull()
            .NotEmpty()
            .MinimumLength(3);
    }
}
