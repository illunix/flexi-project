namespace FlexiProject.Application.Commands.Users.Handlers;

public sealed class DeleteUserCommandHandler(IFlexiProjectDbContext ctx) : ICommandHandler<DeleteUserCommand>
{
    public async ValueTask<Unit> Handle(
        DeleteUserCommand cmd,
        CancellationToken ct
    )
    {
        var user = await ctx.Users.FindAsync(cmd.UserId) ??
            throw new EntityNotFoundException(
                nameof(User),
                cmd.UserId
            );

        ctx.Remove(user);

        await ctx.SaveChangesAsync(ct);

        return Unit.Value;
    }
}