using Ardalis.GuardClauses;
using Ardalis.Result;
using DrugStore.Domain.ProductAggregate;
using DrugStore.Domain.ProductAggregate.Primitives;
using DrugStore.Domain.SharedKernel;
using DrugStore.Persistence;

namespace DrugStore.Application.Products.Commands.UpdateMainImageCommand;

public sealed class UpdateMainImageCommandHandler(Repository<Product> repository)
    : ICommandHandler<UpdateMainImageCommand, Result<ProductId>>
{
    public async Task<Result<ProductId>> Handle(UpdateMainImageCommand request, CancellationToken cancellationToken)
    {
        var product = await repository.GetByIdAsync(request.ProductId, cancellationToken);
        Guard.Against.NotFound(request.ProductId, product);

        var image = product.Images?.FirstOrDefault(i => i.ImageUrl == request.ImageUrl);
        Guard.Against.NotFound(request.ImageUrl, image);

        if (image.IsMain == request.IsMain)
            return Result<ProductId>.Success(product.Id);

        image.IsMain = request.IsMain;

        if (request.IsMain)
            product.Images?
                .Where(i => i.ImageUrl != request.ImageUrl)
                .ToList()
                .ForEach(i => i.IsMain = false);

        await repository.UpdateAsync(product, cancellationToken);

        return Result<ProductId>.Success(product.Id);
    }
}