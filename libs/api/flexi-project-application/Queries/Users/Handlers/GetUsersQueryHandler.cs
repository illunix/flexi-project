namespace FlexiProject.Application.Queries.Users.Handlers;

public sealed class GetUsersQueryHandler(
    IFlexiProjectDbContext ctx,
    UserMapper mapper
) : IQueryHandler<GetUsersQuery, Paged<UserDto>>
{
    public async ValueTask<Paged<UserDto>> Handle(
        GetUsersQuery qry,
        CancellationToken ct
    )
        => await ctx.Users
            .AsNoTracking()
            .Select(q => mapper.MapUserToUserDto(q))
            .ToPaged(
                qry.PageNumber,
                qry.PageSize, 
                ct
            );
}
