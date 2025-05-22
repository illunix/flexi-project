namespace FlexiProject.Core.Entities;

public sealed class User : EntityBase
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public string? Avatar { get; set; }
}
