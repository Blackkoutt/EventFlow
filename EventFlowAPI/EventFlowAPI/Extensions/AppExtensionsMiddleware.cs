using EventFlowAPI.Middleware;

namespace EventFlowAPI.Extensions
{
    public static class AppExtensionsMiddleware
    {
        public static void AddApplicationMiddleware(this WebApplication app)
        {
            app.UseMiddleware<UnauthorizedResponseMiddleware>();
            app.UseMiddleware<ForbiddenResponseMiddleware>();
        }
    }
}
