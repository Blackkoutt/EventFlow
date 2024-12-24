using EventFlowAPI.Extensions;
using EventFlowAPI.Logic.Mapper.Profiles;
using QuestPDF.Infrastructure;
using Serilog;


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
builder.Services.AddControllers();
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
    //builder.Configuration.AddUserSecrets<Program>();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.AddApplicationMiddleware();
app.UseSession();

app.UseCors("AllowSpecificOrigins");

app.UseAuthentication();
app.UseAuthorization();

app.UseAutoMapper();

app.MapControllers();

app.Run();