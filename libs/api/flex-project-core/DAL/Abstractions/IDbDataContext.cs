namespace FlexiProject.Core.DAL.Abstractions;

public interface IDbDataContext : IDisposable
{
    Task<int> SaveChangesAsync(CancellationToken ct = default);
    void Add<T>(T entity) where T : class;
    void Remove<T>(T entity) where T : class;
}