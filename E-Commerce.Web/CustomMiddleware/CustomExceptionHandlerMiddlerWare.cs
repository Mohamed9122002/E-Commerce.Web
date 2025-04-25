using DomainLayer.Exceptions;
using Shared.ErrorModels;
using System.Net;
using System.Text.Json;

namespace E_Commerce.Web.CustomeMiddleware
{
    public class CustomExceptionHandlerMiddlerWare(RequestDelegate _next, ILogger<CustomExceptionHandlerMiddlerWare> _logger)
    {

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next.Invoke(httpContext);
            }
            catch (Exception ex )
            {
                _logger.LogError(ex, "Something Went Wrong");
                // Change
                // Set status code For Respoense 
                //httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.StatusCode = ex switch
                {
                    NotFoundException => StatusCodes.Status404NotFound,
                    _=> StatusCodes.Status500InternalServerError
                };
                // Set Content Type For Resposnse
                httpContext.Response.ContentType = "application/json";
                // Response Object
                var Response = new ErrorToReturn()
                {
                    StatusCode = httpContext.Response.StatusCode,
                    ErrorMessage = ex.Message
                };
                // Return Object As Json 
                //var ResponseToReturn = JsonSerializer.Serialize(Response);

                //await httpContext.Response.WriteAsync(ResponseToReturn);

                await httpContext.Response.WriteAsJsonAsync(Response);

            }

        }
    }
}
