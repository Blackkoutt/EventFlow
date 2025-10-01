using EventFlowAPI.Enums;

namespace EventFlowAPI.Extensions
{
    public static class AppExtensionsCors
    {
        public static void UseCors(this IApplicationBuilder app, CORSPolicy corsPolicy)
        {
            app.UseCors(corsPolicy.ToString());
        }
    }
}
