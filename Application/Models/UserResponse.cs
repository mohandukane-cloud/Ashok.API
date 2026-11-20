namespace Application.Models;

public sealed record UserResponse(int Id, string Username, string Email, DateTime CreatedAtUtc);
