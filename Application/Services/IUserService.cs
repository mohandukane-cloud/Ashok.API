using Application.Models;

namespace Application.Services;

public interface IUserService
{
    Task<UserResponse?> RegisterAsync(RegisterUserRequest request, CancellationToken cancellationToken);
    Task<UserResponse?> LoginAsync(LoginRequest request, CancellationToken cancellationToken);
}
