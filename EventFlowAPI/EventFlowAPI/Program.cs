using EventFlowAPI.Extensions;
using EventFlowAPI.Logic.Mapper.Profiles;

var builder = WebApplication.CreateBuilder(args);

// Add API context options
builder.AddConnectionToDB("EventFlowDB");

// UnitOfWork
builder.Services.AddUnitOfWork();

// App CRUD Services
builder.Services.AddApplicationCRUDServices();

// Other Services
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


