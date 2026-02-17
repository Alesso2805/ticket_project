using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketSupport.Domain.Exceptions;

namespace TicketSupport.API.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception has occurred.");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var response = new ProblemDetails
            {
                Title = "An error occurred while processing your request.",
                Detail = exception.Message,
                Instance = context.Request.Path
            };

            switch (exception)
            {
                case NotFoundException _:
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    response.Status = (int)HttpStatusCode.NotFound;
                    response.Title = "Resource Not Found";
                    break;
                
                case FluentValidation.ValidationException validationEx:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Status = (int)HttpStatusCode.BadRequest;
                    response.Title = "Validation Error";
                    response.Extensions["errors"] = validationEx.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });
                    break;

                default:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    response.Status = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
