﻿using FluentValidation;
using Newtonsoft.Json;
using Refit;
using System.Data.SqlClient;
using System.Net;
using Template.Api.Controllers;

namespace Template.Api.Middleware
{
    public class ResponseExceptionHandler : BaseController
    {
        private readonly RequestDelegate _next;

        public ResponseExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception error)
            {
                switch (error)
                {
                    case ValidationException ex: Response(httpContext, new List<string>() { ex.Message }, ex); break;
                    case ArgumentException ex: Response(httpContext, new List<string>() { ex.Message }, ex); break;
                    case SqlException ex: Response(httpContext, new List<string>() { ex.Message }, ex); break;
                    case ApiException ex: Response(httpContext, new List<string>() { ex.Message, ex.Content }, ex); break;
                    default: Response(httpContext, new List<string>() { error.Message }, error); break;
                }
            }
        }

        public new void Response(HttpContext context, IReadOnlyCollection<string> errors, Exception ex = null)
        {
            var body = JsonConvert.SerializeObject(new
            {
                success = false,
                errors,
                ex = ex == null ? null : new { ex.Message, ex.StackTrace, ex.Source }
            });
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Response.ContentType = "text/json";
            context.Response.WriteAsync(body);
        }
    }

    public static class ResponseExceptionHandlerExtensions
    {
        public static IApplicationBuilder UseResponseExceptionHandler(this IApplicationBuilder builder) => builder.UseMiddleware<ResponseExceptionHandler>();
    }
}
