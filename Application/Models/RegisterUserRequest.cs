namespace Application.Models;

public sealed record RegisterUserRequest(string Username, string Email, string Password);
