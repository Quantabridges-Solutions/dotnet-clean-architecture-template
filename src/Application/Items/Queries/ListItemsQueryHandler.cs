using CleanArchitecture.Domain.Interfaces;
using MediatR;

namespace CleanArchitecture.Application.Items.Queries;

public class ListItemsQueryHandler(IItemRepository repository) : IRequestHandler<ListItemsQuery, IReadOnlyList<ListItemsResult>>
{
    public async Task<IReadOnlyList<ListItemsResult>> Handle(ListItemsQuery request, CancellationToken cancellationToken)
    {
        var items = await repository.ListAsync(cancellationToken);
        return items.Select(i => new ListItemsResult(i.Id, i.Name, i.Description)).ToList();
    }
}
