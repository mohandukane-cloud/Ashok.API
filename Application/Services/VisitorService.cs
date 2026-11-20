using Application.Interfaces;
using Application.Models;
using Domain.Entities;

namespace Application.Services;

public sealed class VisitorService(
    IVisitorRepository visitorRepository,
    IUserRepository userRepository) : IVisitorService
{
    public async Task<IReadOnlyList<VisitorResponse>> GetAllAsync(CancellationToken cancellationToken)
    {
        var visitors = await visitorRepository.GetAllAsync(cancellationToken);
        return visitors.Select(ToResponse).ToArray();
    }

    public async Task<VisitorResponse?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var visitor = await visitorRepository.GetByIdAsync(id, cancellationToken);
        return visitor is null ? null : ToResponse(visitor);
    }

    public async Task<VisitorResponse?> CreateAsync(
        VisitorRequest request,
        CancellationToken cancellationToken)
    {
        if (await userRepository.GetByIdAsync(request.UserId, cancellationToken) is null)
        {
            return null;
        }

        var visitor = await visitorRepository.CreateAsync(
            new Visitor(request.UserId, request.Name, request.Email, request.VisitDateUtc, request.Purpose),
            cancellationToken);
        return ToResponse(visitor);
    }

    public async Task<bool> UpdateAsync(
        int id,
        VisitorRequest request,
        CancellationToken cancellationToken)
    {
        if (await userRepository.GetByIdAsync(request.UserId, cancellationToken) is null)
        {
            return false;
        }

        var visitor = await visitorRepository.GetByIdAsync(id, cancellationToken);
        if (visitor is null)
        {
            return false;
        }

        visitor.Update(request.Name, request.Email, request.VisitDateUtc, request.Purpose);
        await visitorRepository.UpdateAsync(visitor, cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var visitor = await visitorRepository.GetByIdAsync(id, cancellationToken);
        if (visitor is null)
        {
            return false;
        }

        await visitorRepository.DeleteAsync(visitor, cancellationToken);
        return true;
    }

    private static VisitorResponse ToResponse(Visitor visitor) =>
        new(visitor.Id, visitor.UserId, visitor.Name, visitor.Email, visitor.VisitDateUtc, visitor.Purpose);
}
