using System;
using Infrastructure.Common.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace Infrastructure.Common.StartupFilters
{
    public class ResponceStartupFilter:IStartupFilter
    {
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return app =>
            {
                app.UseMiddleware<ResponseLoggingMiddleware>();
                next(app);
            };
        }
    }
}