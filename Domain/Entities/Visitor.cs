namespace Domain.Entities;

public sealed class Visitor
{
    private Visitor()
    {
    }

    public Visitor(int userId, string name, string email, DateTime visitDateUtc, string purpose)
    {
        UserId = userId;
        Name = name;
        Email = email;
        VisitDateUtc = visitDateUtc;
        Purpose = purpose;
    }

    public int Id { get; private set; }
    public int UserId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public DateTime VisitDateUtc { get; private set; }
    public string Purpose { get; private set; } = string.Empty;

    public void Update(string name, string email, DateTime visitDateUtc, string purpose)
    {
        Name = name;
        Email = email;
        VisitDateUtc = visitDateUtc;
        Purpose = purpose;
    }
}
