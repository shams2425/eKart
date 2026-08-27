using UsersService.Core.DTOs;

namespace UsersService.Core.ServiceContracts;

public interface IUserService
{
    public Task<AuthenticationResponse> Login(LoginRequest loginRequest);
    public Task<AuthenticationResponse> Register(RegisterRequest registerRequest);
}
