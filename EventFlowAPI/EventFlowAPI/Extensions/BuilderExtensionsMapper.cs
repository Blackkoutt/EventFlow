using AutoMapper;
using EventFlowAPI.Logic.Mapper.Extensions;

namespace EventFlowAPI.Extensions
{
    public static class BuilderExtensionsMapper
    {
        public static void UseAutoMapper(this IApplicationBuilder app)
        {
            var mapper = app.ApplicationServices.GetService<IMapper>();
            if (mapper != null)
            {
                MappingExtensions.Configure(mapper);
            }
        }
    }
}
