namespace FlexiProject.Application.Queries.Users.Handlers;

public sealed class GetUserDetailsQueryHandler(
    IFlexiProjectDbContext ctx,
    UserMapper mapper
) : IQueryHandler<GetUserDetailsQuery, UserDetailsDto?>
{
    public async ValueTask<UserDetailsDto?> Handle(
        GetUserDetailsQuery qry,
        CancellationToken ct
    )
        => await ctx.Users
            .AsNoTracking()
            .Where(q => q.Id == qry.UserId)
            .Select(q => mapper.MapUserToUserDetailsDto(q))
            .FirstOrDefaultAsync(ct);
}
