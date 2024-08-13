using AutoMapper;
using EventFlowAPI.DB.Context;
using EventFlowAPI.Logic.Mapper.Extensions;
using EventFlowAPI.Logic.Services.Interfaces;
using EventFlowAPI.Logic.Services.Services;
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
        public static void AddApplicationCRUDServices(this IServiceCollection services)
        {
            services.AddScoped<IAdditionalServicesService, AdditionalServicesService>();
            services.AddScoped<IEquipmentService, EquipmentService>();
            services.AddScoped<IEventCategoryService, EventCategoryService>();
            services.AddScoped<IEventDetailsService, EventDetailsService>();
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
    }
}
