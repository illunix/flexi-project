namespace FlexiProject.Application.Queries.Users.Handlers;

public sealed class GetUsersQueryHandler(
    IFlexiProjectDbContext ctx,
    UserMapper mapper
) : IQueryHandler<GetUsersQuery, IEnumerable<UserDto>>
{
    public async ValueTask<IEnumerable<UserDto>> Handle(
        GetUsersQuery qry, 
        CancellationToken ct
    )
        => await ctx.Users
            .AsNoTracking()
            .Select(q => mapper.MapUserToUserDto(q))
            .ToListAsync(ct);
}
