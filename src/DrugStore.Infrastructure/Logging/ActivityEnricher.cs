using Serilog.Core;
using Serilog.Events;
using System.Diagnostics;

namespace DrugStore.Infrastructure.Logging;

public class ActivityEnricher : ILogEventEnricher
{
    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        var activity = Activity.Current;

        logEvent.AddPropertyIfAbsent(new("SpanId", new ScalarValue(activity?.GetSpanId())));
        logEvent.AddPropertyIfAbsent(new("TraceId", new ScalarValue(activity?.GetTraceId())));
        logEvent.AddPropertyIfAbsent(new("ParentId", new ScalarValue(activity?.GetParentId())));
    }
}