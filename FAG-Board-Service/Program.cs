using System.Diagnostics;
using FAG_Board_Service.Contracts;
using FAG_Board_Service.Database;
using FAG_Board_Service.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddLogging();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IGameManagementService, GameManagementService>();
builder.Services.AddScoped<IPlayGameService, PlayGameService>();
builder.Services.AddScoped<IGameDatabaseAccess, GameDatabaseAccess>();

builder.Services.AddDbContext<GameContext>(options =>
{
    options.UseNpgsql(Environment.GetEnvironmentVariable("PostGres_Connectionstring")?.Trim('\"') ??
                      throw new Exception("missing configuration for postgres conenctionstring"));
});


var app = builder.Build();
// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();