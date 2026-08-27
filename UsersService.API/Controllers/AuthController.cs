using Microsoft.AspNetCore.Mvc;
using UsersService.Core.DTOs;
using UsersService.Core.ServiceContracts;

namespace eCommerce.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;

    public AuthController(IUserService userService)
    {
        _userService = userService;
    }
    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginRequest loginRequest)
    {
        if (loginRequest == null)
        {
            return BadRequest("Invalid login data");
        }
        AuthenticationResponse authenticationResponse = await _userService.Login(loginRequest);

        if (authenticationResponse == null)
        {
            return Unauthorized(authenticationResponse);
        }
        return Ok(authenticationResponse);
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register(RegisterRequest registerRequest)
    {
        if (registerRequest is null)
        {
            return BadRequest("Invalid Registration data");
        }

        AuthenticationResponse authenticationResponse = await _userService.Register(registerRequest);

        if (authenticationResponse is null)
        {
            return BadRequest(authenticationResponse);
        }
        return Ok(registerRequest);
    }
}
