namespace FlexiProject.Application.DTOs.Users;

public sealed record UpdateUserDto : UserMutateDto
{
    [JsonIgnore]
    public Guid? Id { get; init; }
}