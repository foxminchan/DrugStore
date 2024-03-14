using Polly;
using Polly.Retry;

namespace DrugStore.BackOffice.Delegates;

public sealed class RetryDelegate : DelegatingHandler
{
    private readonly AsyncRetryPolicy<HttpResponseMessage> _retryPolicy
        = Policy<HttpResponseMessage>
            .Handle<HttpRequestException>()
            .RetryAsync(3);

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var result =
            await _retryPolicy.ExecuteAndCaptureAsync(async () => await base.SendAsync(request, cancellationToken));

        if (result.Outcome == OutcomeType.Failure)
            throw new HttpRequestException("Failed to send request", result.FinalException);

        return result.Result;
    }
}