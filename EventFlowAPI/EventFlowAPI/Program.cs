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

// CORS
builder.Services.AddCorsPolicy();

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

// Test services
app.AddServicesTests();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //builder.Configuration.AddUserSecrets<Program>();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.AddApplicationMiddleware();

app.UseCors("AllowSpecificOrigins");

app.UseAuthentication();
app.UseAuthorization();

app.UseAutoMapper();

app.MapControllers();

app.Run();