using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Assessment.API.Filters
{
    [ExcludeFromCodeCoverage]
    public class ExceptionFilters : IExceptionFilter
    {
        private readonly ILogger<ExceptionFilters> _logger;

        public ExceptionFilters(ILogger<ExceptionFilters> logger)
        {
            _logger = logger;
        }
        public void OnException(ExceptionContext context)
        {
            if (!context.ExceptionHandled && context.Exception is ValidationException validationException)
            {
                _logger.LogError(validationException.Message);
                
                context.Result = new BadRequestObjectResult(new 
                {
                    ResponseDescription = validationException.Message,
                    ResponseCode = "01"
                });
                context.ExceptionHandled = true;
            }
            
            if (!context.ExceptionHandled)
            {
                _logger.LogError($"{context.Exception.Message}|||{context.Exception.InnerException}");
                
                context.Result = new BadRequestObjectResult(new 
                {
                    ResponseDescription = $"{context.Exception.Message}|||{context.Exception.InnerException}",
                    ResponseCode = "01"
                });
                context.ExceptionHandled = true;
            }
        }
    }
}