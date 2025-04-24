using API.Extensions;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Microsoft.Extensions.Logging;
using Serilog;
using Infrastructure.Logging;
using Serilog.Filters;
using Microsoft.Extensions.DependencyInjection;
using Core.Interfaces;
using Infrastructure.Repositories;
using Pomelo.EntityFrameworkCore.MySql;
using API.Services;

var builder = WebApplication.CreateBuilder(args);

var appName = builder.Configuration["SystemName:AppName"] ?? "WorkersApp";

builder.Logging.ClearProviders();

builder.Logging.AddFilter("Microsoft", LogLevel.Warning);
builder.Logging.AddFilter("System", LogLevel.Warning);

// Serilog Configuration
Log.Logger = new LoggerConfiguration()
    .WriteTo.File($"logs/{appName}-.log", rollingInterval: RollingInterval.Day) 
    .Filter.ByExcluding(Matching.FromSource("Microsoft.EntityFrameworkCore")) 
    .Filter.ByExcluding(Matching.FromSource("Microsoft.AspNetCore")) 
    .CreateLogger();

builder.Logging.AddSerilog();

// Force automatically generated paths (as with [controller]) to be lowercase
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Add services to the container.
builder.Services.ConfigureCors(builder.Configuration);
builder.Services.AddAplicacionServices();
builder.Services.AddControllers();

// Configure DbContext with MySQL
builder.Services.AddDbContext<Context>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("LockersConnection"),
        new MySqlServerVersion(new Version(8, 0, 23))
    )
);

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
        logger.LogError(ex, "An error occurred during migration");
    }
}

app.UseCors("CorsPolicy");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();