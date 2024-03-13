using DrugStore.Domain.CategoryAggregate.Primitives;

namespace DrugStore.WebAPI.Endpoints.Product;

public class GetProductByCategoryRequest(CategoryId id, int pageIndex, int pageSize)
{
    public CategoryId Id { get; set; } = id;
    public int PageIndex { get; set; } = pageIndex;
    public int PageSize { get; set; } = pageSize;
}