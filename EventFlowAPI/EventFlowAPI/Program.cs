using EventFlowAPI.DB.Entities;
using EventFlowAPI.Extensions;
using EventFlowAPI.Logic.DTO.Interfaces;
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

    var newEventPass = new EventPass
    {
        Id = 1,
        EventPassGuid = Guid.NewGuid(),
        StartDate = DateTime.Now,
        RenewalDate = null,
        EndDate = DateTime.Now.AddMonths(1),
        PaymentDate = DateTime.Now,
        PaymentAmount = 50,
        PassType = new EventPassType
        {
            Id = 1,
            Name = "Karnet miesiêczny",
            ValidityPeriodInMonths = 1,
            Price = 50
        },
        User = new User
        {
            Id = "1",
            Name = "Mateusz",
            Surname = "Strapczuk",
            Email = "mateusz.strapczuk@gmail.com"
        },
        PaymentType = new PaymentType
        {
            Id = 1,
            Name = "P³atnoœæ kart¹"
        }
    };

    byte[] eventPassBitmap = await assetService.GetOutputBitmap(TestsOutput.EventPass, EventFlowAPI.Logic.Helpers.Enums.ImageFormat.JPEG);

    await pdfbuilder.CreateEventPassPdf(newEventPass, eventPassBitmap);
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