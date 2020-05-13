using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NetCore31.ErrorHandling.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _requestDelegate;

        public ErrorHandlerMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _requestDelegate(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            var httpStatusCode = HttpStatusCode.InternalServerError;

            // START Mapping exceptions
            if (ex is ArgumentException) httpStatusCode = HttpStatusCode.BadRequest;
            else if (ex is FileNotFoundException) httpStatusCode = HttpStatusCode.BadRequest;
            // END Mapping exceptions

            var result = JsonConvert.SerializeObject(new { error = $"StatusCode: '{httpStatusCode}' Message: '{ex.Message}'" });
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)httpStatusCode;
            return httpContext.Response.WriteAsync(result);
        }
    }
}
