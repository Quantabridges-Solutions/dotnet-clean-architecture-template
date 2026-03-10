using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Domain.Interfaces;

public interface IItemRepository
{
    Task<Item?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Item>> ListAsync(CancellationToken cancellationToken = default);
    Task<Item> AddAsync(Item entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(Item entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(Item entity, CancellationToken cancellationToken = default);
}
