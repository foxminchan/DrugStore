using System.Data;
using System.Diagnostics;
using System.Text.Json;
using DrugStore.Domain.SharedKernel;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace DrugStore.Persistence;

[DebuggerStepThrough]
public class TxBehavior<TRequest, TResponse>(
    IPublisher publisher,
    IDatabaseFacade databaseFacade,
    IDomainEventContext eventContext,
    ILogger<TxBehavior<TRequest, TResponse>> logger) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (request is not ITxRequest)
        {
            return await next();
        }

        logger.LogInformation("{Request} handled command {CommandName}", request, request.GetType().Name);
        logger.LogDebug("{Request} handled command {CommandName} with {CommandData}", request, request.GetType().Name,
            request);
        logger.LogInformation("{Request} begin transaction for command {CommandName}", request, request.GetType().Name);

        IExecutionStrategy strategy = databaseFacade.Database.CreateExecutionStrategy();

        return await strategy.ExecuteAsync(async () =>
        {
            await using IDbContextTransaction transaction = await databaseFacade.Database
                .BeginTransactionAsync(IsolationLevel.ReadCommitted, cancellationToken);

            logger.LogInformation("{Request} transaction {TransactionId} begin for command {CommandName}", request,
                transaction.TransactionId, request.GetType().Name);

            List<DomainEventBase> domainEvents = eventContext.GetDomainEvents().ToList();

            logger.LogInformation(
                "{Request} transaction {TransactionId} begin for command {CommandName} with {DomainEventsCount} domain events",
                request, transaction.TransactionId, request.GetType().Name, domainEvents.Count);

            IEnumerable<Task> tasks = domainEvents.Select(async
                domainEvent =>
            {
                await publisher.Publish(
                    new EventWrapper(domainEvent), cancellationToken);

                logger.LogDebug(
                    "{Prefix} Published domain event {DomainEventName} with payload {DomainEventContent}",
                    nameof(TxBehavior<TRequest, TResponse>), domainEvent.GetType().FullName,
                    JsonSerializer.Serialize(domainEvent));
            });

            await Task.WhenAll(tasks).ConfigureAwait(false);

            TResponse response = await next();
            await transaction.CommitAsync(cancellationToken);
            return response;
        }).ConfigureAwait(false);
    }
}
