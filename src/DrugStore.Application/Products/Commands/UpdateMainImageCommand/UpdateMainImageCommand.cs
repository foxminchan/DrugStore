using Ardalis.Result;
using DrugStore.Domain.ProductAggregate.Primitives;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Application.Products.Commands.UpdateMainImageCommand;

public sealed record UpdateMainImageCommand(ProductId ProductId, string ImageUrl, bool IsMain = true)
    : ICommand<Result<ProductId>>;