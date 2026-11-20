namespace Application.Models;

public sealed record VisitorResponse(
    int Id,
    int UserId,
    string Name,
    string Email,
    DateTime VisitDateUtc,
    string Purpose);
