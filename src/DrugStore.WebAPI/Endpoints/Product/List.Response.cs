using Ardalis.Result;

namespace DrugStore.WebAPI.Endpoints.Product;

public sealed class ListProductResponse
{
    public PagedInfo? PagedInfo { get; set; }
    public List<ProductDto>? Products { get; set; } = [];
}