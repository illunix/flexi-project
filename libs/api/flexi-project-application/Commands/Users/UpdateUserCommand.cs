namespace FlexiProject.Application.Commands.Users;

public sealed record UpdateUserCommand(UpdateUserDto User) : ICommand<UserDto>;