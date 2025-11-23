namespace CareerLens.Application.Models
{
    public record PageResultModel<T>(
        T Data,
        int TotalItems,
        int Page,
        int PageSize
    );
}
