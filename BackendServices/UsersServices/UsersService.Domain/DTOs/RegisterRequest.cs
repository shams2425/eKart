namespace UsersService.Core.DTOs;

public record RegisterRequest(string? Email, string? Password, string? PersonName,GenderOption Gender)
{
}
