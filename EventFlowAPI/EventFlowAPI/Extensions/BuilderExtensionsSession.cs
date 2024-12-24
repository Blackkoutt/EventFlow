using ScottPlot.Statistics;

namespace EventFlowAPI.Extensions
{
    public static class BuilderExtensionsSession
    {
        public static void AddAPISession(this IServiceCollection services)
        {
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
        }
    }
}
