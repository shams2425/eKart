using FluentValidation;
using ProductService.BussinessLayer.DTOs;

namespace ProductService.BussinessLayer.Validators;

public class ProductUpdateRequestValidator : AbstractValidator<ProductUpdateRequest>
{
    public ProductUpdateRequestValidator()
    {
        RuleFor(req => req.ProductName)
            .NotEmpty().WithMessage("Product name can't be blank");

        RuleFor(req => req.Category)
            .IsInEnum().WithMessage("Invalid Category");

        RuleFor(req => req.UnitPrice)
            .InclusiveBetween(0, double.MaxValue).WithMessage($"UnitPrice must be between 0 and {double.MaxValue}");

        RuleFor(req => req.QuantityStock)
            .InclusiveBetween(1, int.MaxValue).WithMessage($"Quantity in stock must be between 0 and {int.MaxValue}");
    }
}
