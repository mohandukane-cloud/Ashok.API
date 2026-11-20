using Application.Models;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("users")]
public sealed class UserController(IUserService userService) : ControllerBase
{
    [HttpPost("register")]
    [ProducesResponseType<UserResponse>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UserResponse>> Register(
        RegisterUserRequest request,
        CancellationToken cancellationToken)
    {
        var user = await userService.RegisterAsync(request, cancellationToken);
        return user is null ? BadRequest("Username is already in use or the request is invalid.") :
            CreatedAtAction(nameof(Register), new { user.Id }, user);
    }

    [HttpPost("login")]
    [ProducesResponseType<UserResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<UserResponse>> Login(
        LoginRequest request,
        CancellationToken cancellationToken)
    {
        var user = await userService.LoginAsync(request, cancellationToken);
        return user is null ? Unauthorized("Invalid username or password.") : Ok(user);
    }
}
