using FluentValidation;
using ModularWebApi.Modules.Orders.Application.Commands;

namespace ModularWebApi.Modules.Orders.Application.Validators
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator() 
        {
            RuleFor(x => x.order)
                .NotNull().WithMessage("Order can't be null.");
        }
    }
}
