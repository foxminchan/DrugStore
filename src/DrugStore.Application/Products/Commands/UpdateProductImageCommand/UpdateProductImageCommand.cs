using Ardalis.Result;
using DrugStore.Domain.ProductAggregate.Primitives;
using DrugStore.Domain.SharedKernel;
using Microsoft.AspNetCore.Http;

namespace DrugStore.Application.Products.Commands.UpdateProductImageCommand;

public sealed record UpdateProductImageCommand(ProductId ProductId, List<IFormFile> Images)
    : ICommand<Result<ProductId>>;