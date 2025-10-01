using EventFlowAPI.Enums;
using EventFlowAPI.Extensions;
using EventFlowAPI.Logic.Enums;
using EventFlowAPI.Logic.Mapper.Profiles;
using QuestPDF.Infrastructure;
using Serilog;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Set QuestPDF License
QuestPDF.Settings.License = LicenseType.Community;

// Add connection to DB
builder.AddConnectionToDB(ConnectionString.MSSQLEventFlowDB);

// Add connection to Azure Blob Storage
builder.AddConnectionToAzureBlobStorage(ConnectionString.AzureBlobStorage);

// Add Identity
builder.AddIdentity();

// Add JWT Token, Google Auth and Facebook Auth
builder.AddAuthentication(AuthConfiguration.JWTAuth, AuthConfiguration.GoogleAuth, AuthConfiguration.FacebookAuth);

// Logger
Log.Logger = new LoggerConfiguration().MinimumLevel.Information().WriteTo.Console().CreateLogger();

// UnitOfWork
builder.Services.AddUnitOfWork();

// CORS
builder.Services.AddCorsPolicy();

// App Services
builder.Services.AddApplicationCRUDServices();
builder.Services.AddApplicationAuthServices();
builder.Services.AddApplicationOtherServices();

// Other Services
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerUI();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddHttpContextAccessor();
builder.Services.AddAPISession();

var app = builder.Build();

// Test services
app.AddServicesTests();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.AddApplicationMiddleware();
app.UseSession();

app.UseCors(CORSPolicy.AllowSpecificOrigins);

app.UseAuthentication();
app.UseAuthorization();

app.UseAutoMapper();

app.MapControllers();

app.Run();