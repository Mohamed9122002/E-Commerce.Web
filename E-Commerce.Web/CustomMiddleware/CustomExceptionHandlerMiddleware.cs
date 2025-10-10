using DomainLayer.Exceptions;
using Shared.ErrorModels;
using System.Net;
using System.Text.Json;

namespace E_Commerce.Web.CustomMiddleware
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomExceptionHandlerMiddleware> _logger;

        public CustomExceptionHandlerMiddleware(RequestDelegate Next, ILogger<CustomExceptionHandlerMiddleware> logger)
        {
            _next = Next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next.Invoke(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something Went Wrong");
                // Set Status Code For Response
                httpContext.Response.StatusCode = ex switch
                {
                    NotFoundException => StatusCodes.Status404NotFound,
                    _=> StatusCodes.Status500InternalServerError 
                };
                // Set Content Type For Response 
                httpContext.Response.ContentType = "application/json";
                // Response Object  =>C#
                var Response = new ErrorToReturn()
                {
                    StatusCode = httpContext.Response.StatusCode,
                    ErrorMessage = ex.Message
                };
                // Return Object As JSON 
                //  var ResponseToReturn = JsonSerializer.Serialize(Response);
                //await   httpContext.Response.WriteAsync(ResponseToReturn);
                await httpContext.Response.WriteAsJsonAsync(Response);
            }
        }
    }
}
