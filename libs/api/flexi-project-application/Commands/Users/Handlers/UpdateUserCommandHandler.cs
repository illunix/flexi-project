namespace FlexiProject.Application.Commands.Users.Handlers;

public sealed class UpdateUserCommandHandler(
    IFlexiProjectDbContext ctx,
    UserMapper mapper
) : ICommandHandler<UpdateUserCommand, UserDto>
{
    public async ValueTask<UserDto> Handle(
        UpdateUserCommand cmd,
        CancellationToken ct
    )
    {
        var user = await ctx.Users.FindAsync(cmd.User.Id) ??
            throw new EntityNotFoundException(
                nameof(User),
                cmd.User.Id
            );

        mapper.MapUpdateUserDtoToUser(
            cmd.User, 
            user
        );

        await ctx.SaveChangesAsync(ct);

        return mapper.MapUserToUserDto(user);
    }
}