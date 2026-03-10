using CleanArchitecture.Domain.Interfaces;
using MediatR;

namespace CleanArchitecture.Application.Items.Queries;

public class GetItemQueryHandler(IItemRepository repository, ICacheService cache) : IRequestHandler<GetItemQuery, GetItemResult?>
{
    public async Task<GetItemResult?> Handle(GetItemQuery request, CancellationToken cancellationToken)
    {
        var cacheKey = $"item:{request.Id}";
        var cached = await cache.GetAsync<GetItemResult>(cacheKey, cancellationToken);
        if (cached is not null)
            return cached;

        var item = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (item is null)
            return null;

        var result = new GetItemResult(item.Id, item.Name, item.Description, item.CreatedAtUtc);
        await cache.SetAsync(cacheKey, result, TimeSpan.FromMinutes(5), cancellationToken);
        return result;
    }
}
