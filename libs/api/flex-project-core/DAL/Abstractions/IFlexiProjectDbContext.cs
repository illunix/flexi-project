namespace FlexiProject.Core.DAL.Abstractions;

public interface IFlexiProjectDbContext : IDbDataContext
{
    public DbSet<User> Users { get; init; }
}