namespace FlexiProject.Application.Commands.Users.Handlers;

public sealed class CreateUserCommandHandler(
    IFlexiProjectDbContext ctx,
    UserMapper mapper
) : ICommandHandler<CreateUserCommand, UserDto>
{
    public async ValueTask<UserDto> Handle(
        CreateUserCommand cmd,
        CancellationToken ct
    )
    {
        var user = mapper.MapCreateUserDtoToUser(cmd.User);

        ctx.Add(user);

        await ctx.SaveChangesAsync(ct);

        return mapper.MapUserToUserDto(user);
    }
}