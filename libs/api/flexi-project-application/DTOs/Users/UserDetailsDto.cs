namespace FlexiProject.Application.DTOs.Users;

public sealed record UserDetailsDto
{
    public required string Name { get; init; }
    public required string Email { get; init; }
    public string? Avatar { get; init; }
}