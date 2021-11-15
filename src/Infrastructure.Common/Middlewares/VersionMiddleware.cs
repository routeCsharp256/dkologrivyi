using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Common.Middlewares
{
    public class VersionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<VersionMiddleware> _logger;

        public VersionMiddleware(ILogger<VersionMiddleware> logger, RequestDelegate next)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var version = Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "no version";
            var serviceName = Assembly.GetCallingAssembly().GetName().Name;
            var result = new {version = version, serviceName = serviceName};
            context.Response.StatusCode = 200;
            await context.Response.WriteAsync(result.ToString());
        }
        
        
        
    }
}