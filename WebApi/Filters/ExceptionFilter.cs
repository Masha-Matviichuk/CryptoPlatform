using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WebApi.Models.ErrorModels;

namespace Api.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<ExceptionFilter> _logger;
        private readonly IHostEnvironment _environment;

        ExceptionFilter(IHostEnvironment environment, ILogger<ExceptionFilter> logger)
        {
            _environment = environment;
            _logger = logger;
        }
        
        public void OnException(ExceptionContext context)
        {
            
            
            var error = new ApiError();

            if (_environment.IsDevelopment())
            {
                error.Message = context.Exception.Message;
                error.Details = context.Exception.StackTrace;
            }
            else
            {
                error.Message = "A server error occured.";
                error.Details = context.Exception.Message;
            }
            
            
            _logger.LogError("Unhandled exception occurred while executing request: {Ex}", context.Exception);
            
            context.Result = new ObjectResult(error)
            {
                StatusCode = 500
            };
            
            
        }
    }
}