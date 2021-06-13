using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DemoProject.Backend.Middleware
{
    public class DebugMiddleware
    {
        private readonly RequestDelegate _next;

        public DebugMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Debug.WriteLine(context.Request.Path.Value);
            await _next(context);
        }
    }

    public static class DebugMiddlewareExtension
    {
        public static IApplicationBuilder UseDebugMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<DebugMiddleware>();
        }
    }
}
