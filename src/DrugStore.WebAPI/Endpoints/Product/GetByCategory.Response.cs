using Ardalis.Result;

namespace DrugStore.WebAPI.Endpoints.Product;

public sealed class GetProductByCategoryResponse
{
    public PagedInfo? PagedInfo { get; set; }
    public List<ProductDto>? Products { get; set; } = [];
}