using FluentValidation;
using PracticeInventory.Application.Inventory.Commands;

namespace PracticeInventory.Application.Inventory.Validator;

public class UpdateInventoryCommandValidator : AbstractValidator<UpdateInventoryCommand>
{
    public UpdateInventoryCommandValidator()
    {
        RuleFor(x => x.ItemId)
          .NotNull()
          .NotEmpty();
        RuleFor(x => x.Cost)
          .NotNull()
          .NotEmpty()
          .GreaterThan(0);
        RuleFor(x => x.SalePrice)
          .NotNull()
          .NotEmpty()
          .GreaterThan(0);
        RuleFor(x => x.CurrentQuantity)
          .NotNull()
          .NotEmpty();
        RuleFor(x => x.CriticalQuantity)
          .NotNull()
          .NotEmpty();
    }
}
