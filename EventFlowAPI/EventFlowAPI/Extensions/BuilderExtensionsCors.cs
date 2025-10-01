using EventFlowAPI.Enums;

namespace EventFlowAPI.Extensions
{
    public static class BuilderExtensionsCors
    {
        public static void AddCorsPolicy(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(CORSPolicy.AllowSpecificOrigins.ToString(), policy =>
                {
                    policy.WithOrigins("http://localhost:5173")
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials();
                });
            });
        }
    }
}
