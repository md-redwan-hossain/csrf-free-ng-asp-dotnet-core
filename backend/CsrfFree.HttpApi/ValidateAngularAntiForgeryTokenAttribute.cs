using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CsrfFree.HttpApi;

[AttributeUsage(AttributeTargets.Method)]
public class ValidateAngularAntiForgeryTokenAttribute : Attribute, IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var antiForgery = (IAntiforgery)context
            .HttpContext.RequestServices
            .GetRequiredService(typeof(IAntiforgery));

        if (await antiForgery.IsRequestValidAsync(context.HttpContext) is false)
        {
            context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
            return;
        }

        await next();
    }
}