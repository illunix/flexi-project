namespace FlexiProject.Application.Pagination;

public sealed record Paged<TData>
{
    public int CurrentPage { get; init; }
    public int PageCount { get; init; }
    public int PageSize { get; init; }
    public IEnumerable<TData> List { get; init; } = Enumerable.Empty<TData>();
    public int DataCount { get; init; }
}
