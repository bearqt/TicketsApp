using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace TicketsApp.Middlewares
{
    public class ValidateJsonSizeMiddleware
    {
        private readonly RequestDelegate _next;

        public ValidateJsonSizeMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        
        public Task InvokeAsync(HttpContext httpContext)
        {
            httpContext.Request.EnableBuffering();
            long maxJsonSize = 2000;
            var jsonSize = httpContext.Request.ContentLength;
            if (jsonSize > maxJsonSize)
            {
                httpContext.Response.StatusCode = 413;
            }

            httpContext.Request.Body.Position = 0;
            return _next(httpContext);
        }
    }

    public static class ValidateJsonMiddlewareExtensions
    {
        public static IApplicationBuilder UseValidateJsonSizeMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ValidateJsonSizeMiddleware>();
        }
    
}

    
}