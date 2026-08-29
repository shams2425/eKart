using FluentValidation;
using UsersService.Core.DTOs;

namespace UsersService.Core.Validators;

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(lr => lr.Email).NotEmpty().WithMessage("Email is required");
        RuleFor(lr => lr.Email).EmailAddress().WithMessage("Email is not correct format");
    }
}
