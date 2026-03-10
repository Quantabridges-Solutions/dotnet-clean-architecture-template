using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces;
using MediatR;

namespace CleanArchitecture.Application.Items.Commands;

public class CreateItemCommandHandler(IItemRepository repository) : IRequestHandler<CreateItemCommand, CreateItemResult>
{
    public async Task<CreateItemResult> Handle(CreateItemCommand request, CancellationToken cancellationToken)
    {
        var item = new Item
        {
            Name = request.Name,
            Description = request.Description
        };
        var created = await repository.AddAsync(item, cancellationToken);
        return new CreateItemResult(created.Id);
    }
}
