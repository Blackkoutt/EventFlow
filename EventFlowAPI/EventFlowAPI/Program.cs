using EventFlowAPI.DB.Entities;
using EventFlowAPI.Extensions;
using EventFlowAPI.Logic.Helpers.Enums;
using EventFlowAPI.Logic.Mapper.Profiles;
using EventFlowAPI.Logic.Services.OtherServices.Interfaces;
using EventFlowAPI.Logic.Services.OtherServices.Services;
using EventFlowAPI.Logic.UnitOfWork;
using EventFlowAPI.Middleware;
using QuestPDF.Infrastructure;


var builder = WebApplication.CreateBuilder(args);


// Set QuestPDF License
QuestPDF.Settings.License = LicenseType.Community;

// Add connection to DB
builder.AddConnectionToDB(connectionString: "MSSQLEventFlowDB");

// Add connection to Azure Blob Storage
builder.AddConnectionToAzureBlobStorage(connectionString: "AzureBlobStorage");

// Add Identity
builder.AddIdentity();

// Add JWT Token, Google Auth and Facebook Auth
builder.AddAuthentication(jwtSettingsSection: "JWTSettings",
                          googleAuthSection: "Authentication:Google",
                          facebookAuthSection: "Authentication:Facebook");

// UnitOfWork
builder.Services.AddUnitOfWork();

// App Services
builder.Services.AddApplicationCRUDServices();
builder.Services.AddApplicationAuthServices();
builder.Services.AddApplicationOtherServices();

// Other Services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerUI();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

/*using (var scope = app.Services.CreateScope())
{
    var assetService = scope.ServiceProvider.GetRequiredService<IAssetService>();
    var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

    var pdfbuilder = new PdfBuilderService(assetService, unitOfWork);

    var reservationEntity = new Reservation
    {
        Id = 1233141,
        ReservationGuid = Guid.NewGuid(),
        ReservationDate = DateTime.Now,
        TotalAddtionalPaymentPercentage = 55m,
        TotalAdditionalPaymentAmount = 25m,
        User = new User
        {
            Name = "Mateusz",
            Surname = "Strapczuk"
        },
        PaymentAmount = 125,
        PaymentType = new PaymentType
        {
            Name = "P³atnoœæ kart¹"
        },
        Ticket = new Ticket
        {
            Event = new Event
            {
                Name="Nowe wydarzenie",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1),
                Category = new EventCategory
                {
                    Name = "Koncert"
                },
                Hall = new Hall
                {
                    Id = 1,
                    HallNr = 2
                }
            },
            TicketType = new TicketType
            {
                Name = "Ulgowy"
            },
            Price = 25
            
        }
    };
    List<byte[]> tickets = [];
    var ticketOut = await assetService.GetOutputBitmap(TestsOutput.EventPath, EventFlowAPI.Logic.Helpers.Enums.ImageFormat.JPEG);
    tickets.Add(ticketOut);
    tickets.Add(ticketOut);
    tickets.Add(ticketOut);

    await pdfbuilder.CreateTicketPdf(reservationEntity, tickets);
}*/

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.AddApplicationMiddleware();

app.UseAuthentication();
app.UseAuthorization();

app.UseAutoMapper();

app.MapControllers();

app.Run();


