using FAG_Board_Service.Contracts;
using FAG_Board_Service.Database;
using FAG_Board_Service.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IGameManagementService, GameManagementService>();
builder.Services.AddSingleton<IPlayGameService, PlayGameService>();
builder.Services.AddSingleton<IGameDatabaseAccess, GameDatabaseAccess>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();