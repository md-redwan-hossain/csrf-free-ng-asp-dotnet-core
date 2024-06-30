using Microsoft.AspNetCore.Antiforgery;

namespace CsrfFree.HttpApi;

public class CsrfCookieMiddleware : IMiddleware
{
    private readonly IAntiforgery _antiForgery;

    public CsrfCookieMiddleware(IAntiforgery antiForgery)
    {
        _antiForgery = antiForgery;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        if (HttpMethods.IsPost(context.Request.Method) ||
            HttpMethods.IsPut(context.Request.Method) ||
            HttpMethods.IsPatch(context.Request.Method) ||
            HttpMethods.IsDelete(context.Request.Method))
        {
            var token = _antiForgery.GetAndStoreTokens(context);
            context.Response.Headers.Append("X-XSRF-TOKEN", token.RequestToken);
        }

        await next(context);
    }
}