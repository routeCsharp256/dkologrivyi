using System;
using Infrastructure.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace Infrastructure.StartupFilters
{
    public class ReadyStartupFilter:IStartupFilter
    {
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return app =>
            {
                app.Map("/ready", builder => builder.UseMiddleware<ReadyMiddleware>());
                next(app);
            };
        }
    }
}