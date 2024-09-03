using EventFlowAPI.Logic.Identity.Services.Interfaces;
using EventFlowAPI.Logic.Identity.Services.Services;
using EventFlowAPI.Logic.Services.Interfaces;
using EventFlowAPI.Logic.Services.Services;
using EventFlowAPI.Logic.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace EventFlowAPI.Extensions
{
    public static class BuilderExtentionsServices
    {
        public static void AddUnitOfWork(this IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
        public static void AddApplicationCRUDServices(this IServiceCollection services)
        {
            services.AddScoped<IAdditionalServicesService, AdditionalServicesService>();
            services.AddScoped<IEquipmentService, EquipmentService>();
            services.AddScoped<IEventCategoryService, EventCategoryService>();
            services.AddScoped<IEventPassService, EventPassService>();
            services.AddScoped<IEventPassTypeService, EventPassTypeService>();
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IEventTicketService, EventTicketService>();
            services.AddScoped<IFestivalDetailsService, FestivalDetailsService>();
            services.AddScoped<IFestivalService, FestivalService>();
            services.AddScoped<IHallRentService, HallRentService>();
            services.AddScoped<IHallService, HallService>();
            services.AddScoped<IHallTypeService, HallTypeService>();
            services.AddScoped<IMediaPatronService, MediaPatronService>();
            services.AddScoped<IOrganizerService, OrganizerService>();
            services.AddScoped<IPaymentTypeService, PaymentTypeService>();
            services.AddScoped<IReservationService, ReservationService>();
            services.AddScoped<ISeatService, SeatService>();
            services.AddScoped<ISeatTypeService, SeatTypeService>();
            services.AddScoped<ISponsorService, SponsorService>();
            services.AddScoped<ITicketTypeService, TicketTypeService>();
            services.AddScoped<IUserDataService, UserDataService>();
            services.AddScoped<IUserService, UserService>();
        }
        public static void AddApplicationAuthServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IGoogleAuthService, GoogleAuthService>();
            services.AddScoped<IFacebookAuthService, FacebookAuthService>();
            services.AddSingleton<IJWTGeneratorService, JWTGeneratorService>();
        }
        public static void AddSwaggerUI(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
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
            });
        }
        private static OpenApiSecurityScheme GetApiSecurityScheme()
        {
            return new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                Description = "Enter JWT token:"
            };
        }
    }
}
