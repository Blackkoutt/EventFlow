using EventFlowAPI.Extensions;
using EventFlowAPI.Logic.Mapper.Profiles;

var builder = WebApplication.CreateBuilder(args);

// Add API context options
builder.AddConnectionToDB(connectionString: "EventFlowDB");

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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseAutoMapper();

app.MapControllers();

app.Run();


