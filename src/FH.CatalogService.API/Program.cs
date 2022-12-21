using FH.CatalogService.API;
using FH.CatalogService.Application.Abstraction;
using FH.CatalogService.Infrastructure;
using Microsoft.EntityFrameworkCore;
using NF.ExchangeRates.Core;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAPIServices();
builder.Services.AddApplicatrionServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});


builder.Services.AddStackExchangeRedisCache(redisOptions =>
{
    var connection = builder.Configuration.GetConnectionString("Redis");
    redisOptions.Configuration = connection;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// Initialise and seed database
using (var scope = app.Services.CreateScope())
{
    var initialiser = scope.ServiceProvider.GetRequiredService<DatabaseContextInitialiser>();
    await initialiser.InitialiseAsync();
    await initialiser.SeedAsync();
}



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
