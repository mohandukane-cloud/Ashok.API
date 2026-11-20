using Application.Interfaces;
using Application.Models;
using Application.Security;
using Domain.Entities;

namespace Application.Services;

public sealed class UserService(IUserRepository userRepository) : IUserService
{
    public async Task<UserResponse?> RegisterAsync(
        RegisterUserRequest request,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Username) ||
            string.IsNullOrWhiteSpace(request.Email) ||
            string.IsNullOrWhiteSpace(request.Password))
        {
            return null;
        }

        if (await userRepository.GetByUsernameAsync(request.Username, cancellationToken) is not null)
        {
            return null;
        }

        var user = await userRepository.CreateAsync(
            new User(request.Username.Trim(), request.Email.Trim(), PasswordHasher.Hash(request.Password)),
            cancellationToken);

        return ToResponse(user);
    }

    public async Task<UserResponse?> LoginAsync(
        LoginRequest request,
        CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByUsernameAsync(request.Username, cancellationToken);
        return user is not null && PasswordHasher.Verify(request.Password, user.PasswordHash)
            ? ToResponse(user)
            : null;
    }

    private static UserResponse ToResponse(User user) =>
        new(user.Id, user.Username, user.Email, user.CreatedAtUtc);
}
