namespace FlexiProject.Application.DTOs.Users;

public record UserMutateDto
{
    public required string Name { get; init; }
    public required string Email { get; init; }
    public required string Avatar { get; init; }
}