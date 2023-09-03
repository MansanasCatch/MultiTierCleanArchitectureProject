using FluentValidation;
using PracticeInventory.Application.Item.Commands;

namespace PracticeInventory.Application.Item.Validator;

public class UpdateItemCommandValidator : AbstractValidator<UpdateItemCommand>
{
    public UpdateItemCommandValidator()
    {
        RuleFor(x => x.CategoryId)
            .NotNull()
            .NotEmpty();
        RuleFor(x => x.ItemName)
            .NotNull()
            .NotEmpty();
    }
}
