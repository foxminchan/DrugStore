﻿namespace DrugStore.BackOffice.Delegates;

public sealed class LoggingDelegate(ILogger<LoggingDelegate> logger) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        try
        {
            var response = await base.SendAsync(request, cancellationToken);
            response.EnsureSuccessStatusCode();
            logger.LogInformation("Request: {RequestMethod} {RequestUri} {RequestContent}", request.Method,
                request.RequestUri, response);
            return response;
        }
        catch (HttpRequestException ex)
        {
            logger.LogError(ex, "[{RequestMethod}] {RequestUri} has error: {ErrorMessage}", request.Method,
                request.RequestUri, ex.Message);
            throw new HttpRequestException(ex.Message);
        }
    }
}