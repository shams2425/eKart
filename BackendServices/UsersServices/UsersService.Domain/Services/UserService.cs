using AutoMapper;
using UsersService.Core.DTOs;
using UsersService.Core.Entities;
using UsersService.Core.RepositoriesContracts;
using UsersService.Core.ServiceContracts;

namespace UsersService.Core.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<AuthenticationResponse> Login(LoginRequest loginRequest)
    {
        ApplicationUser user = await _userRepository.GetUserByEmailAndPAssword(loginRequest.Email,loginRequest.Password);
        if (user == null)
        {
            return null;
        }
        return _mapper.Map<AuthenticationResponse>(user);
    }

    public async Task<AuthenticationResponse> Register(RegisterRequest registerRequest)
    {
       ApplicationUser user = _mapper.Map<ApplicationUser>(registerRequest);

       ApplicationUser registerdUser = await _userRepository.AddUser(user);
        if (registerdUser is null)
        {
            return null;
        }
        return _mapper.Map<AuthenticationResponse>(registerdUser);
    }
}
