using System.Text.Json;
using Ardalis.Result;
using DrugStore.Domain.ProductAggregate;
using DrugStore.Domain.ProductAggregate.Primitives;
using DrugStore.Domain.SharedKernel;
using DrugStore.Infrastructure.Storage.Local;
using DrugStore.Persistence.Repositories;
using Microsoft.Extensions.Logging;

namespace DrugStore.Application.Products.Commands.CreateProductCommand;

public sealed class CreateProductCommandHandler(
    IRepository<Product> repository,
    ILogger<CreateProductCommandHandler> logger,
    ILocalStorage localStorage) : IIdempotencyCommandHandler<CreateProductCommand, Result<ProductId>>
{
    public async Task<Result<ProductId>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var result = string.Empty;

        if (request.Image is not null)
            result = await localStorage.UploadFileAsync(request.Image, cancellationToken);

        Product product = new(
            request.Name,
            request.ProductCode,
            request.Detail,
            request.Quantity,
            request.CategoryId,
            request.ProductPrice,
            string.IsNullOrWhiteSpace(result) ? null : new(result, request.Alt ?? request.Name, request.Name)
        );

        logger.LogInformation("[{Command}] Product information: {Product}", nameof(CreateProductCommand),
            JsonSerializer.Serialize(product));

        await repository.AddAsync(product, cancellationToken);

        return Result<ProductId>.Success(product.Id);
    }
}