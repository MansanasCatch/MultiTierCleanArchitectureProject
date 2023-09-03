using FluentValidation;
using PracticeInventory.Application.Account.Commands;
using PracticeInventory.Application.Item.Commands;

namespace PracticeInventory.Application.Item.Validator;

public class AddItemCommandValidator : AbstractValidator<AddItemCommand>
{
    public AddItemCommandValidator()
    {
        RuleFor(x => x.CategoryId)
            .NotNull()
            .NotEmpty();
        RuleFor(x => x.ItemName)
            .NotNull()
            .NotEmpty();
    }
}
