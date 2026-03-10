namespace CleanArchitecture.Domain.Entities;

public class Item : BaseEntity
{
    public required string Name { get; set; }
    public string? Description { get; set; }
}
