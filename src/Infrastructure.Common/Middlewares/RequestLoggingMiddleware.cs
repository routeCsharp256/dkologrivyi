using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Common.Middlewares
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await LogRequest(context);
            await _next(context);
        }

        private async Task LogRequest(HttpContext context)
        {
            try
            {
                context.Request.EnableBuffering();
                var route = context.Request.Path.Value;
                var headers = context.Request.Headers;

                var result = new StringBuilder();
                result.AppendLine("Request logged");
                result.AppendLine(route);
                foreach (var header in headers)
                {
                    result.AppendLine(header.Key + " " + header.Value);
                }


                if (context.Request.ContentLength > 0)
                {
                    var buffer = new byte[context.Request.ContentLength.Value];
                    await context.Request.Body.ReadAsync(buffer, 0, buffer.Length);
                    var bodyAsText = Encoding.UTF8.GetString(buffer);
                    result.AppendLine(bodyAsText);
                }

                _logger.LogInformation(result.ToString());

                context.Request.Body.Position = 0;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Could not log request body");
            }
        }
    }
}