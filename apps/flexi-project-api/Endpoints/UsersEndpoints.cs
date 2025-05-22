namespace FlexiProject.API.Endpoints;

internal static class UsersEndpoints
{
    public static RouteGroupBuilder MapUsersEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("users");

        group
            .MapGet(
                "/",
                async (
                    int page,
                    int pageSize,
                    ISender sender,
                    CancellationToken ct
                ) => Results.Ok(await sender.Send(
                    new GetUsersQuery()
                    {
                        PageNumber = page,
                        PageSize = pageSize
                    },
                    ct
                ))
            )
            .WithName("GetUsers")
            .WithSummary("Users get operation")
            .Produces<Paged<UserDto>>()
            .Produces(StatusCodes.Status400BadRequest);

        group
            .MapGet(
                "/{userId}",
                async (
                    Guid userId,
                    ISender sender,
                    CancellationToken ct
                ) =>
                {
                    var user = await sender.Send(
                        new GetUserDetailsQuery(userId),
                        ct
                    );
                    if (user is null)
                        return Results.NotFound();

                    return Results.Ok(user);
                }
            )
            .WithName("GetUserDetails")
            .WithSummary("User details get operation")
            .Produces<UserDetailsDto>()
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound);

        group
            .MapPost(
                "/",
                async (
                    CreateUserDto user,
                    ISender sender,
                    CancellationToken ct
                ) => Results.Created(string.Empty, await sender.Send(
                    new CreateUserCommand(user),
                    ct
                ))
            )
            .WithName("CreateUser")
            .WithSummary("User create operation")
            .Produces<UserDto>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status422UnprocessableEntity)
            .Produces(StatusCodes.Status400BadRequest);

        group
            .MapPut(
                "/{userId}",
                async (
                    Guid userId,
                    UpdateUserDto user,
                    ISender sender,
                    CancellationToken ct
                ) => Results.Ok(await sender.Send(
                    new UpdateUserCommand(user with { Id = userId }),
                    ct
                ))
            )
            .WithName("UpdateUser")
            .WithSummary("User update operation")
            .Produces<UserDto>()
            .Produces(StatusCodes.Status422UnprocessableEntity)
            .Produces(StatusCodes.Status400BadRequest);

        group
            .MapDelete(
                "/{userId}",
                async (
                    Guid userId,
                    ISender sender,
                    CancellationToken ct
                ) => Results.Ok(await sender.Send(
                    new DeleteUserCommand(userId),
                    ct
                ))
            )
            .WithName("DeleteUser")
            .WithSummary("User delete operation")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest);

        group.WithTags("Users");

        return group;
    }
}
