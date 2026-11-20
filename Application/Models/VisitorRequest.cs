namespace Application.Models;

public sealed record VisitorRequest(
    int UserId,
    string Name,
    string Email,
    DateTime VisitDateUtc,
    string Purpose);
