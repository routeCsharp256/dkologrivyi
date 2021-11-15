using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Common.Middlewares
{
    public class LiveMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LiveMiddleware> _logger;

        public LiveMiddleware(RequestDelegate next, ILogger<LiveMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task Invoke(HttpContext context)
        {
            context.Response.StatusCode = 200;
            await context.Response.CompleteAsync();
        }


    }
}