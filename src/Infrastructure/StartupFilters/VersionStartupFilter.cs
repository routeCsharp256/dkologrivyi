using System;
using Infrastructure.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace Infrastructure.StartupFilters
{
    public class VersionStartupFilter : IStartupFilter
    {
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return app =>
            {
                app.Map("/version", builder => builder.UseMiddleware<VersionMiddleware>());
                next(app);
            };
        }
    }
}