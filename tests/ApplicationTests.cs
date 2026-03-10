using CleanArchitecture.Application.Items.Commands;
using CleanArchitecture.Application.Items.Queries;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces;
using FluentAssertions;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using CleanArchitecture.Application;
using Xunit;

namespace CleanArchitecture.Tests;

public class CreateItemCommandHandlerTests
{
    private readonly IMediator _mediator;
    private readonly Mock<IItemRepository> _repository = new();

    public CreateItemCommandHandlerTests()
    {
        var services = new ServiceCollection();
        services.AddApplication();
        services.AddTransient(_ => _repository.Object);
        services.AddTransient(_ => Mock.Of<ICacheService>());
        _mediator = services.BuildServiceProvider().GetRequiredService<IMediator>();
    }

    [Fact]
    public async Task Handle_ValidCommand_ReturnsId()
    {
        var item = new Item { Id = Guid.NewGuid(), Name = "Test", Description = "Desc" };
        _repository.Setup(r => r.AddAsync(It.IsAny<Item>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(item);

        var result = await _mediator.Send(new CreateItemCommand("Test", "Desc"));

        result.Id.Should().Be(item.Id);
        _repository.Verify(r => r.AddAsync(It.Is<Item>(e => e.Name == "Test" && e.Description == "Desc"), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_EmptyName_ThrowsValidationException()
    {
        var act = () => _mediator.Send(new CreateItemCommand("", "Desc"));
        await act.Should().ThrowAsync<ValidationException>();
        _repository.Verify(r => r.AddAsync(It.IsAny<Item>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}
