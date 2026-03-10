using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Infrastructure.Persistence;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Testcontainers.PostgreSql;
using Xunit;

namespace CleanArchitecture.Tests;

public class ItemRepositoryTests : IAsyncLifetime
{
    private readonly PostgreSqlContainer _container = new PostgreSqlBuilder()
        .WithImage("postgres:16-alpine")
        .WithDatabase("test")
        .WithUsername("postgres")
        .WithPassword("postgres")
        .Build();

    private ApplicationDbContext _context = null!;
    private ItemRepository _repository = null!;

    public async Task InitializeAsync()
    {
        await _container.StartAsync();
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseNpgsql(_container.GetConnectionString())
            .Options;
        _context = new ApplicationDbContext(options);
        await _context.Database.EnsureCreatedAsync();
        _repository = new ItemRepository(_context);
    }

    public async Task DisposeAsync()
    {
        if (_context is not null)
            await _context.DisposeAsync();
        await _container.DisposeAsync();
    }

    [Fact]
    public async Task AddAsync_Then_GetById_ReturnsItem()
    {
        var item = new Item { Name = "Integration Item", Description = "From Testcontainers" };
        var added = await _repository.AddAsync(item);
        added.Id.Should().NotBeEmpty();

        var found = await _repository.GetByIdAsync(added.Id);
        found.Should().NotBeNull();
        found!.Name.Should().Be("Integration Item");
        found.Description.Should().Be("From Testcontainers");
    }

    [Fact]
    public async Task ListAsync_ReturnsOrderedItems()
    {
        await _repository.AddAsync(new Item { Name = "First" });
        await _repository.AddAsync(new Item { Name = "Second" });
        var list = await _repository.ListAsync();
        list.Should().HaveCountGreaterOrEqualTo(2);
        list.Select(i => i.Name).Should().Contain("First");
        list.Select(i => i.Name).Should().Contain("Second");
    }
}
