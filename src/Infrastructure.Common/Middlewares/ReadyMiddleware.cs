using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Common.Middlewares
{
    public class ReadyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ReadyMiddleware> _logger;

        public ReadyMiddleware(RequestDelegate next, ILogger<ReadyMiddleware> logger)
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