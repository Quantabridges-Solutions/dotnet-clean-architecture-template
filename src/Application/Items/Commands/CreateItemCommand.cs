using MediatR;

namespace CleanArchitecture.Application.Items.Commands;

public record CreateItemCommand(string Name, string? Description) : IRequest<CreateItemResult>;

public record CreateItemResult(Guid Id);
