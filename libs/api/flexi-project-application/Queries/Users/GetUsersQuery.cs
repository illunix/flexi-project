namespace FlexiProject.Application.Queries.Users;

public sealed record GetUsersQuery : IQuery<IEnumerable<UserDto>>;