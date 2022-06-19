using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace TicketsApp.Middlewares
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
            catch (BadHttpRequestException ex)
            {
                if (ex.Message == "Request body too large.")
                {
                    context.Response.StatusCode = 413;
                    await context.Response.WriteAsync(new ErrorDetail()
                    {
                        StatusCode = 413,
                        Message = "JSON size is bigger than 2kb"
                    }.ToString());
                }
                else if (ex.Message == "Invalid input value(s)")
                {
                    context.Response.StatusCode = 400;
                    await context.Response.WriteAsync(new ErrorDetail()
                    {
                        StatusCode = 400,
                        Message = "Invalid input value(s)."
                    }.ToString());
                }
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is PostgresException npgex &&
                    npgex.SqlState == PostgresErrorCodes.UniqueViolation)
                {
                    context.Response.StatusCode = 409;
                    await context.Response.WriteAsync(new ErrorDetail()
                    {
                        StatusCode = 409,
                        Message = "Ticket is already sold"
                    }.ToString());
                }
                else
                {
                    context.Response.StatusCode = 409;
                    await context.Response.WriteAsync(new ErrorDetail()
                    {
                        StatusCode = 409,
                        Message = "Ticket has already been refund or doesnt exist"
                    }.ToString());
                }
            }
            catch (TaskCanceledException ex)
            {
                if (ex.Message == "A task was canceled.")
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
    }

    public static class ErrorMiddlewareExtension
    {
        public static IApplicationBuilder UseErrorHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlerMiddleware>();
        }
    }
}