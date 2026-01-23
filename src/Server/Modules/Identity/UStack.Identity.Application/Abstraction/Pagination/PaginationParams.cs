namespace UStack.Identity.Application.Abstraction.Pagination;

public class PaginationParams
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;

    private const int MaxPageSize = 100;

    public void SetPageSize(int size)
    {
        PageSize = size > MaxPageSize ? MaxPageSize : size;
    }
}
