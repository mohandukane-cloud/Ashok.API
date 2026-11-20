using Application.Models;

namespace Application.Services;

public interface IVisitorService
{
    Task<IReadOnlyList<VisitorResponse>> GetAllAsync(CancellationToken cancellationToken);
    Task<VisitorResponse?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<VisitorResponse?> CreateAsync(VisitorRequest request, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(int id, VisitorRequest request, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
}
