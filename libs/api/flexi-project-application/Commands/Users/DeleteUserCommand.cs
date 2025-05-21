namespace FlexiProject.Application.Commands.Users;

public sealed record DeleteUserCommand(Guid UserId) : ICommand;