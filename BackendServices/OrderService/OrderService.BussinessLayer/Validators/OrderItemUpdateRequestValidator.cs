using FluentValidation;
using OrdersService.BussinessLayer.DTOs;

namespace OrdersService.BussinessLayer.Validators;

public class OrderItemUpdateRequestValidator : AbstractValidator<OrderItemUpdateRequest>
{
    public OrderItemUpdateRequestValidator()
    {
        // ProductID
        RuleFor(temp => temp.ProductID)
            .NotEmpty()
            .WithMessage("Product ID can't be blank");

        // UnitPrice
        RuleFor(temp => temp.UnitPrice)
            .GreaterThan(0)
            .WithMessage("Unit Price must be greater than zero");

        // Quantity
        RuleFor(temp => temp.Quantity)
            .GreaterThan(0)
            .WithMessage("Quantity must be greater than zero");
    }
}
