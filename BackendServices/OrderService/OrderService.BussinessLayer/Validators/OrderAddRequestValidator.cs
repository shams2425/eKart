using FluentValidation;
using OrdersService.BussinessLayer.DTOs;

namespace OrdersService.BusinessLogicLayer.Validators;

public class OrderAddRequestValidator : AbstractValidator<OrderAddRequest>
{
    public OrderAddRequestValidator()
    {
        //UserID
        RuleFor(temp => temp.UserId)
          .NotEmpty().WithMessage("User ID can't be blank");

        //OrderDate
        RuleFor(temp => temp.OrderDate)
          .NotEmpty().WithMessage("Order Date can't be blank");

        //OrderItems
        RuleFor(temp => temp.OrderItem)
          .NotEmpty().WithMessage("Order Items can't be blank");
    }
}