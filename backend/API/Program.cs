using API.Extensions;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Microsoft.Extensions.Logging;
using Serilog;
using Infrastructure.Logging;
using Serilog.Filters;
using Microsoft.Extensions.DependencyInjection;
using Core.Interfases;
using Infrastructure.Repositories;
using Pomelo.EntityFrameworkCore.MySql;
using Core.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();

builder.Logging.AddFilter("Microsoft", LogLevel.Warning);
builder.Logging.AddFilter("System", LogLevel.Warning);

// Serilog Configuration
Log.Logger = new LoggerConfiguration()
    .WriteTo.File("logs/todolist-.log", rollingInterval: RollingInterval.Day) 
    .Filter.ByExcluding(Matching.FromSource("Microsoft.EntityFrameworkCore")) 
    .Filter.ByExcluding(Matching.FromSource("Microsoft.AspNetCore")) 
    .CreateLogger();

builder.Logging.AddSerilog();


builder.Services.AddAutoMapper(Assembly.GetEntryAssembly());
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IDocumentRepository, DocumentRepository>();
builder.Services.AddScoped<ILocationRepository, LocationRepository>();
builder.Services.AddScoped<ILockerRepository, LockerRepository>();
builder.Services.AddScoped<IPriceRepository, PriceRepository>();
builder.Services.AddScoped<IRentRepository, RentRepository>();
builder.Services.AddScoped<LockerDTOService>();
builder.Services.AddScoped<CustomerDTOService>();
builder.Services.AddScoped<RentDTOService>();


// Add services to the container.
builder.Services.ConfigureCors();
builder.Services.AddAplicacionServices();
builder.Services.AddControllers();

// Configure DbContext with MySQL
builder.Services.AddDbContext<Context>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("LockersConnection"),
        new MySqlServerVersion(new Version(8, 0, 23))
    )
);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
    try
    {
        var context = services.GetRequiredService<Context>();
        await context.Database.MigrateAsync();
    }
    catch (Exception ex)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex, "Ocurrió un error durante la migración");
    }
}

app.UseCors("CorsPolicy");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();