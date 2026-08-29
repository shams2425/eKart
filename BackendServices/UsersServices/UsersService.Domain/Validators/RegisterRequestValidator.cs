using FluentValidation;
using UsersService.Core.DTOs;

namespace UsersService.Core.Validators;

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(rr => rr.Email).EmailAddress().WithMessage("Email address is not correct form");
        RuleFor(rr => rr.Password).NotEmpty().WithMessage("Password is not correct");
        RuleFor(rr => rr.PersonName).NotEmpty().WithMessage("Person name is required").Length(1, 50);
        RuleFor(rr => rr.Gender).IsInEnum().WithMessage("Invalid gender option");
    }
}
