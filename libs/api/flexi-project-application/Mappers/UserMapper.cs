namespace FlexiProject.Application.Mappers;

[Mapper]
public sealed partial class UserMapper
{
    public partial UserDto MapUserToUserDto(User user);
    public partial User MapCreateUserDtoToUser(CreateUserDto user);
    public partial void MapUpdateUserDtoToUser(
        UpdateUserDto dto,
        User user
    );
}