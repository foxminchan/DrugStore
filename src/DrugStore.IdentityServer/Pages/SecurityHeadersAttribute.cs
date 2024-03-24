// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.

using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DrugStore.IdentityServer.Pages;

public sealed class SecurityHeadersAttribute : ActionFilterAttribute
{
    public override void OnResultExecuting(ResultExecutingContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        var result = context.Result;
        if (result is not PageResult) return;

        // https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/X-Content-Type-Options
        if (!context.HttpContext.Response.Headers.ContainsKey("X-Content-Type-Options"))
            context.HttpContext.Response.Headers.Append("X-Content-Type-Options", "nosniff");

        // https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/X-Frame-Options
        if (!context.HttpContext.Response.Headers.ContainsKey("X-Frame-Options"))
            context.HttpContext.Response.Headers.Append("X-Frame-Options", "DENY");

        // https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Content-Security-Policy
        const string csp =
            "default-src 'self'; object-src 'none'; frame-ancestors 'none'; sandbox allow-forms allow-same-origin allow-scripts; base-uri 'self';";

        // also consider adding upgrade-insecure-requests once you have HTTPS in place for production
        // also an example if you need client images to be displayed from twitter
        // once for standards compliant browsers
        if (!context.HttpContext.Response.Headers.ContainsKey("Content-Security-Policy"))
            context.HttpContext.Response.Headers.Append("Content-Security-Policy", csp);
        // and once again for IE
        if (!context.HttpContext.Response.Headers.ContainsKey("X-Content-Security-Policy"))
            context.HttpContext.Response.Headers.Append("X-Content-Security-Policy", csp);

        // https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Referrer-Policy
        const string referrerPolicy = "no-referrer";
        if (!context.HttpContext.Response.Headers.ContainsKey("Referrer-Policy"))
            context.HttpContext.Response.Headers.Append("Referrer-Policy", referrerPolicy);
    }
}