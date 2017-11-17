using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Botwin
{
    public class MethodNotAllowedMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly HashSet<string> _knownRoutes;

        public MethodNotAllowedMiddleware(RequestDelegate next, HashSet<string> knownRoutes)
        {
            _next = next;
            _knownRoutes = knownRoutes;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path.Value.Trim('/');

            if (_knownRoutes.Contains(path))
            {
                context.Response.StatusCode = 405;
            }
            else
            {
                await _next(context);
            }
        }
    }
}
