using System;
using Infrastructure.Common.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace Infrastructure.Common.StartupFilters
{
    public class LiveStartupFilter : IStartupFilter
    {
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return app =>
            {
                app.Map("/live", builder => builder.UseMiddleware<LiveMiddleware>());
                next(app);
            };
        }
    }
}