namespace FlexiProject.Application.Commands.Users;

public sealed record CreateUserCommand(CreateUserDto User) : ICommand<UserDto>;