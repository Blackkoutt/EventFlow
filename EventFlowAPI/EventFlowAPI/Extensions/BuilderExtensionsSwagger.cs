using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace EventFlowAPI.Extensions
{
    public static class BuilderExtensionsSwagger
    {
        public static void AddSwaggerUI(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.AddSwaggerDocumentation();
                options.AddSecurityDefinition();
                options.AddSecurityRequirement();
            });
        }
        private static void AddSwaggerDocumentation(this SwaggerGenOptions options)
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "EventFlowAPI",
                Version = "v1"
            });

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);
        }

        private static void AddSecurityDefinition(this SwaggerGenOptions options)
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Wprowadź token JWT w formacie: Bearer {token}"
            });
        }
        private static void AddSecurityRequirement(this SwaggerGenOptions options)
        {

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
        }
        
    }
}
