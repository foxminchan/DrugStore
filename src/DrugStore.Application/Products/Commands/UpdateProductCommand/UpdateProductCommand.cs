using Ardalis.Result;
using DrugStore.Application.Products.ViewModels;
using DrugStore.Domain.CategoryAggregate.Primitives;
using DrugStore.Domain.ProductAggregate.Primitives;
using DrugStore.Domain.ProductAggregate.ValueObjects;
using DrugStore.Domain.SharedKernel;
using Microsoft.AspNetCore.Http;

namespace DrugStore.Application.Products.Commands.UpdateProductCommand;

public sealed record UpdateProductCommand(
    ProductId Id,
    string Name,
    string? ProductCode,
    string? Detail,
    int Quantity,
    CategoryId? CategoryId,
    ProductPrice ProductPrice,
    bool IsDeleteImage,
    IFormFile? Image,
    string? Alt) : ICommand<Result<ProductVm>>;