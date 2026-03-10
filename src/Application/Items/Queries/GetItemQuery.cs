using MediatR;

namespace CleanArchitecture.Application.Items.Queries;

public record GetItemQuery(Guid Id) : IRequest<GetItemResult?>;

public record GetItemResult(Guid Id, string Name, string? Description, DateTime CreatedAtUtc);
