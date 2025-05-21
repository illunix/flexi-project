using FlexiProject.Core.DAL.Abstractions;

namespace FlexiProject.Core.DAL;

public abstract class DbDataContext(DbContextOptions options) :
    DbContext(options),
    IDbDataContext
{
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return base.SaveChangesAsync(cancellationToken);
    }

    public void Add<T>(T entity) where T : class
        => Set<T>().Add(entity);

    public void Remove<T>(T entity) where T : class
        => Set<T>().Remove(entity);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
}
