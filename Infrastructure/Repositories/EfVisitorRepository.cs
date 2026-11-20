using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public sealed class EfVisitorRepository(WeatherDbContext dbContext) : IVisitorRepository
{
    public async Task<IReadOnlyList<Visitor>> GetAllAsync(CancellationToken cancellationToken) =>
        await dbContext.Visitors.AsNoTracking().OrderBy(visitor => visitor.VisitDateUtc).ToArrayAsync(cancellationToken);

    public Task<Visitor?> GetByIdAsync(int id, CancellationToken cancellationToken) =>
        dbContext.Visitors.SingleOrDefaultAsync(visitor => visitor.Id == id, cancellationToken);

    public async Task<Visitor> CreateAsync(Visitor visitor, CancellationToken cancellationToken)
    {
        dbContext.Visitors.Add(visitor);
        await dbContext.SaveChangesAsync(cancellationToken);
        return visitor;
    }

    public async Task UpdateAsync(Visitor visitor, CancellationToken cancellationToken)
    {
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Visitor visitor, CancellationToken cancellationToken)
    {
        dbContext.Visitors.Remove(visitor);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
