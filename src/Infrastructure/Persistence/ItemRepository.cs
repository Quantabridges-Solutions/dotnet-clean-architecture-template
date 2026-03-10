using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Persistence;

public class ItemRepository(ApplicationDbContext context) : IItemRepository
{
    public async Task<Item?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        await context.Items.FindAsync([id], cancellationToken);

    public async Task<IReadOnlyList<Item>> ListAsync(CancellationToken cancellationToken = default) =>
        await context.Items.OrderBy(x => x.CreatedAtUtc).ToListAsync(cancellationToken);

    public async Task<Item> AddAsync(Item entity, CancellationToken cancellationToken = default)
    {
        context.Items.Add(entity);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task UpdateAsync(Item entity, CancellationToken cancellationToken = default)
    {
        context.Items.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Item entity, CancellationToken cancellationToken = default)
    {
        context.Items.Remove(entity);
        await context.SaveChangesAsync(cancellationToken);
    }
}
