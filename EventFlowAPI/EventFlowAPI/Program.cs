using EventFlowAPI.Extensions;
using EventFlowAPI.Logic.Mapper.Profiles;

var builder = WebApplication.CreateBuilder(args);

// Add API context options
builder.AddConnectionToDB("EventFlowDB");

// UnitOfWork
builder.Services.AddUnitOfWork();

// Services
builder.Services.AddApplicationServices();

/*builder.Services.AddScoped<IAdditionalServicesRepository, AdditionalServicesRepository>();
builder.Services.AddScoped<IEquipmentRepository, EquipmentRepository>();
builder.Services.AddScoped<IEventCategoryRepository, EventCategoryRepository>();
builder.Services.AddScoped<IEventDetailsRepository, EventDetailsRepository>();
builder.Services.AddScoped<IEventPassRepository, EventPassRepository>();
builder.Services.AddScoped<IEventPassTypeRepository, EventPassTypeRepository>();
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IEventTicketRepository, EventTicketRepository>();
builder.Services.AddScoped<IFestival_EventRepository, Festival_EventRepository>();
builder.Services.AddScoped<IFestival_MediaPatronRepository, Festival_MediaPatronRepository>();
builder.Services.AddScoped<IFestival_OrganizerRepository, Festival_OrganizerRepository>();
builder.Services.AddScoped<IFestival_SponsorRepository, Festival_SponsorRepository>();
builder.Services.AddScoped<IFestivalDetailsRepository, FestivalDetailsRepository>();
builder.Services.AddScoped<IFestivalRepository, FestivalRepository>();
builder.Services.AddScoped<IHallRent_AdditionalServicesRepository, HallRent_AdditionalServicesRepository>();
builder.Services.AddScoped<IHallRentRepository, HallRentRepository>();
builder.Services.AddScoped<IHallRepository, HallRepository>();
builder.Services.AddScoped<IHallType_EquipmentRepository, HallType_EquipmentRepository>();
builder.Services.AddScoped<IHallTypeRepository, HallTypeRepository>();
builder.Services.AddScoped<IMediaPatronRepository, MediaPatronRepository>();
builder.Services.AddScoped<IOrganizerRepository, OrganizerRepository>();
builder.Services.AddScoped<IPaymentTypeRepository, PaymentTypeRepository>();
builder.Services.AddScoped<IReservation_SeatRepository, Reservation_SeatRepository>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<ISeatRepository, SeatRepository>();
builder.Services.AddScoped<ISeatTypeRepository, SeatTypeRepository>();
builder.Services.AddScoped<ISponsorRepository, SponsorRepository>();
builder.Services.AddScoped<ITicketTypeRepository, TicketTypeRepository>();
builder.Services.AddScoped<IUserDataRepository, UserDataRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();*/

// Services

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseAutoMapper();

app.MapControllers();

app.Run();
