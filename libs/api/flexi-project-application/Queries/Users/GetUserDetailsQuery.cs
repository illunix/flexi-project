namespace FlexiProject.Application.Queries.Users;

public sealed record GetUserDetailsQuery(Guid UserId) : IQuery<UserDetailsDto?>;