namespace FlexiProject.Application.Pagination;

internal static class Extensions
{
    public static async Task<Paged<T>> ToPaged<T>(
        this IQueryable<T> query,
        int pageNumber,
        int pageSize,
        CancellationToken ct
    )
    {
        try
        {
            var page = pageNumber < 0 ? 0 : pageNumber;

            if (pageSize == 0)
                pageSize = 10;

            var sizes = await query.CountAsync(ct);
            var max = sizes > 0 ? (int)Math.Ceiling(sizes / (double)pageSize) : 1;

            page = page >= max ? max - 1 : page;

            query = query
                .Skip(page * pageSize)
                .Take(pageSize);

            var list = await query.ToListAsync(ct);

            return new Paged<T>
            {
                CurrentPage = page,
                PageSize = list.Count,
                PageCount = max,
                DataCount = sizes,
                List = list
            };
        }
        catch (OperationCanceledException)
        {
            return new Paged<T>();
        }
    }
}