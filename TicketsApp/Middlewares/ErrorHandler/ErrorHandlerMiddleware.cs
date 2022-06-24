using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace TicketsApp.Middlewares.ErrorHandler
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (BadHttpRequestException ex) when (ex.Message == "Request body too large.")
            {
                context.Response.StatusCode = 413;
                await context.Response.WriteAsync(new ErrorDetail()
                {
                    StatusCode = 413,
                    Message = "JSON size is bigger than 2kb"
                }.ToString());
            }
            catch (BadHttpRequestException ex) when (ex.Message == "Invalid input value(s)")
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(new ErrorDetail()
                {
                    StatusCode = 400,
                    Message = ex.Message
                }.ToString());
            }
            catch (DbUpdateException ex) when (ex.InnerException is PostgresException {SqlState: PostgresErrorCodes.UniqueViolation})
            {
                context.Response.StatusCode = 409;
                await context.Response.WriteAsync(new ErrorDetail()
                {
                    StatusCode = 409,
                    Message = ex.Message
                }.ToString());
            }

            catch (DbUpdateException ex) when (ex.Message == "Ticket has already been refund or doesnt exist")
            {
                context.Response.StatusCode = 409;
                await context.Response.WriteAsync(new ErrorDetail()
                {
                    StatusCode = 409,
                    Message = ex.Message
                }.ToString());
            }
            
            catch (TaskCanceledException ex) when (ex.Message == "A task was canceled.")
            {
                context.Response.StatusCode = 408;
                await context.Response.WriteAsync(new ErrorDetail()
                {
                    StatusCode = 408,
                    Message = "Request time has exceeded timeout (120s)"
                }.ToString());
            }
        }
    }
    public static class ErrorMiddlewareExtension
    {
        public static IApplicationBuilder UseErrorHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlerMiddleware>();
        }
    }
}