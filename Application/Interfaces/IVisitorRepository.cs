using Domain.Entities;

namespace Application.Interfaces;

public interface IVisitorRepository
{
    Task<IReadOnlyList<Visitor>> GetAllAsync(CancellationToken cancellationToken);
    Task<Visitor?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<Visitor> CreateAsync(Visitor visitor, CancellationToken cancellationToken);
    Task UpdateAsync(Visitor visitor, CancellationToken cancellationToken);
    Task DeleteAsync(Visitor visitor, CancellationToken cancellationToken);
}
