using FluentValidation;
using OrdersService.BussinessLayer.DTOs;

public class OrderItemAddRequestValidator : AbstractValidator<OrderItemAddRequest>
{
    public OrderItemAddRequestValidator()
    {
        RuleFor(temp => temp.ProductID)
            .NotEmpty()
            .WithMessage("Product ID can't be blank");

        RuleFor(temp => temp.UnitPrice)
            .GreaterThan(0)
            .WithMessage("Unit Price must be greater than zero");

        RuleFor(temp => temp.Quantity)
            .GreaterThan(0)
            .WithMessage("Quantity must be greater than zero");
    }
}
