using System.Data.Common;
using System.Diagnostics;
using System.Text;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;

namespace DrugStore.Persistence.Interceptors;

public sealed class TimingInterceptor(ILogger<TimingInterceptor> logger) : DbCommandInterceptor
{
    private const long MaxAllowedExecutionTime = 5000;
    private readonly Stopwatch _stopwatch = new();

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
        long executionTime = _stopwatch.ElapsedMilliseconds;

        if (executionTime <= MaxAllowedExecutionTime)
        {
            return await base.ReaderExecutingAsync(command, eventData, result, cancellationToken);
        }

        string stackTrace = string.Join("\n", Environment.StackTrace.Split('\n').Select(x => x));
        StringBuilder message = new();

        message.AppendLine("[WARNING] Query took longer than the maximum allowed execution time.");
        message.AppendLine($"Query: {command.CommandText}");
        message.AppendLine($"Execution Time: {executionTime}ms");
        message.AppendLine($"Maximum Allowed Execution Time: {MaxAllowedExecutionTime}ms");
        message.AppendLine("This query should be optimized or split into smaller queries. ");
        message.AppendLine($"Stack Trace: {stackTrace}");

        await using (StreamWriter writer = File.AppendText("interceptors.txt"))
        {
            await writer.WriteLineAsync(message.ToString());
        }

        return await base.ReaderExecutingAsync(command, eventData, result, cancellationToken);
    }
}
