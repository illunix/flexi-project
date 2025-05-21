using FlexiProject.Core.DAL.Abstractions;

namespace FlexiProject.Infrastructure.DAL;

public sealed class FlexiProjectDbContext (DbContextOptions<FlexiProjectDbContext > options) :
    DbDataContext(options),
    IFlexiProjectDbContext
{
    public required DbSet<User> Users { get; init; }

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.UseInMemoryDatabase("FlexiProject");
    }
}
