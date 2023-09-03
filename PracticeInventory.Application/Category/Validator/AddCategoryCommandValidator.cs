using FluentValidation;
using PracticeInventory.Application.Account.Commands;
using PracticeInventory.Application.Category.Commands;

namespace PracticeInventory.Application.Category.Validator;

public class AddCategoryCommandValidator : AbstractValidator<AddCategoryCommand>
{
    public AddCategoryCommandValidator()
    {
        RuleFor(x => x.CategoryName)
            .NotNull()
            .NotEmpty()
            .MinimumLength(5);
    }
}
