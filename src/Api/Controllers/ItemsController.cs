using CleanArchitecture.Application.Items.Commands;
using CleanArchitecture.Application.Items.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItemsController(ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> List(CancellationToken cancellationToken) =>
        Ok(await sender.Send(new ListItemsQuery(), cancellationToken));

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetItemQuery(id), cancellationToken);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateItemRequest request, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new CreateItemCommand(request.Name, request.Description), cancellationToken);
        return CreatedAtAction(nameof(Get), new { id = result.Id }, new { id = result.Id });
    }
}

public record CreateItemRequest(string Name, string? Description);
