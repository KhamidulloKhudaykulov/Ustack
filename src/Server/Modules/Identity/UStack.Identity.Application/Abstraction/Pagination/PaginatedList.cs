namespace UStack.Identity.Application.Abstraction.Pagination;

public class PaginatedList<T>
{
    public List<T> Items { get; private set; }
    public int TotalCount { get; private set; }
    public int PageNumber { get; private set; }
    public int PageSize { get; private set; }
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);

    public PaginatedList(List<T> items, int count, int pageNumber, int pageSize)
    {
        Items = items;
        TotalCount = count;
        PageNumber = pageNumber;
        PageSize = pageSize;
    }

    public static PaginatedList<T> Create(List<T> source, int totalCount, int pageNumber, int pageSize)
    {
        return new PaginatedList<T>(source, totalCount, pageNumber, pageSize);
    }
}
