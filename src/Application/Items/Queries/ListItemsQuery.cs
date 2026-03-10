using MediatR;

namespace CleanArchitecture.Application.Items.Queries;

public record ListItemsQuery : IRequest<IReadOnlyList<ListItemsResult>>;

public record ListItemsResult(Guid Id, string Name, string? Description);
