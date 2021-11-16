using System;
using System.IO;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Common.Middlewares
{
    public class ResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ResponseLoggingMiddleware> _logger;

        public ResponseLoggingMiddleware(RequestDelegate next, ILogger<ResponseLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var originalBody = context.Response.Body;
            using var newBody = new MemoryStream();
            context.Response.Body = newBody;

            try
            {
                await _next(context);
            }
            catch (Exception )
            {
                _logger.LogError("Could not log response body");
                throw;
            }
            finally
            {
                newBody.Seek(0, SeekOrigin.Begin);
                var bodyText = await new StreamReader(context.Response.Body).ReadToEndAsync();
                newBody.Seek(0, SeekOrigin.Begin);
                await newBody.CopyToAsync(originalBody);

                var route = context.Request.Path.Value;
                var headers = context.Response.Headers;

                var result = new StringBuilder();
                result.AppendLine("Response logged");
                result.AppendLine(route);
                foreach (var header in headers)
                {
                    result.AppendLine(header.Key + " " + header.Value);
                }
                result.AppendLine(bodyText);
                _logger.LogInformation(result.ToString());
            }
        }
    }
}