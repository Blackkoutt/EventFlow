using AutoMapper;
using EventFlowAPI.DB.Context;
using EventFlowAPI.Logic.Mapper.Extensions;
using EventFlowAPI.Logic.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace EventFlowAPI.Extensions
{
    public static class BuilderExtensions
    {
        public static void UseAutoMapper(this IApplicationBuilder app)
        {
            var mapper = app.ApplicationServices.GetService<IMapper>();
            if(mapper != null)
            {
                MappingExtensions.Configure(mapper);
            }
        }
        public static void AddConnectionToDB(this WebApplicationBuilder builder, string connectionString)
        {
            builder.Services.AddDbContext<APIContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString(connectionString));
            });
        }
        public static void AddUnitOfWork(this IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
        public static void AddApplicationServices(this IServiceCollection services)
        {
            // here services

        }
    }
}
