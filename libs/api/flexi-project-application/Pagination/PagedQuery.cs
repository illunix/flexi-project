namespace FlexiProject.Application.Pagination;

public abstract record PagedQuery<T> : IQuery<Paged<T>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
