using FluentValidation;
using PracticeInventory.Application.Category.Commands;

namespace PracticeInventory.Application.Category.Validator;

public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidator()
    {
        RuleFor(x => x.CategoryId)
            .NotNull()
            .NotEmpty();
        RuleFor(x => x.CategoryName)
            .NotNull()
            .NotEmpty()
            .MinimumLength(5);
    }
}
