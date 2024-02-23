using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using System.Data.Common;
using System.Diagnostics;
using System.Text;

namespace DrugStore.Persistence.Interceptors;

public sealed class TimingInterceptor(ILogger<TimingInterceptor> logger) : DbCommandInterceptor
{
    private readonly Stopwatch _stopwatch = new();
    private const long MaxAllowedExecutionTime = 5000;

    public override InterceptionResult<DbDataReader> ReaderExecuting(
        DbCommand command,
        CommandEventData eventData,
        InterceptionResult<DbDataReader> result)
    {
        _stopwatch.Restart();
        return base.ReaderExecuting(command, eventData, result);
    }

    public override async ValueTask<InterceptionResult<DbDataReader>> ReaderExecutingAsync(
        DbCommand command,
        CommandEventData eventData,
        InterceptionResult<DbDataReader> result,
        CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Executing query: {Query}", command.CommandText);
        _stopwatch.Stop();
        var executionTime = _stopwatch.ElapsedMilliseconds;

        if (executionTime <= MaxAllowedExecutionTime)
            return await base.ReaderExecutingAsync(command, eventData, result, cancellationToken);

        var stackTrace = string.Join("\n", Environment.StackTrace.Split('\n').Select(x => x));
        var message = new StringBuilder();

        message.AppendLine("[WARNING] Query took longer than the maximum allowed execution time.");
        message.AppendLine($"Query: {command.CommandText}");
        message.AppendLine($"Execution Time: {executionTime}ms");
        message.AppendLine($"Maximum Allowed Execution Time: {MaxAllowedExecutionTime}ms");
        message.AppendLine("This query should be optimized or split into smaller queries. ");
        message.AppendLine($"Stack Trace: {stackTrace}");

        await using (var writer = File.AppendText("interceptors.txt"))
        {
            await writer.WriteLineAsync(message.ToString());
        }

        return await base.ReaderExecutingAsync(command, eventData, result, cancellationToken);
    }
}
