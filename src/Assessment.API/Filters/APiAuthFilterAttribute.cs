using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Assessment.API.Filters
{
    [ExcludeFromCodeCoverage]
    public class APiAuthFilterAttribute : Attribute, IAsyncActionFilter
    {
        private const string ApikeyHeaderName = "ApiKey";
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(ApikeyHeaderName, out var potentialKey))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            var apiKey = configuration.GetValue<string>("ApiKey");
            
            if (!apiKey.Equals(potentialKey))
            {
                context.Result = new UnauthorizedResult();
                return;
            }
            
            await next();
        }
    }
}