using Core.Interfases;
using Infrastructure.Repositories;
using Infrastructure.UnitOfWork;

namespace API.Extensions;
public static class ApplicationServiceExtensions
{
    public static void ConfigureCors(this IServiceCollection services) =>
        services.AddCors(options =>
        {
        string[] verbs = ["GET", "POST", "PUT", "DELETE", "PATCH", "OPTIONS"];
            options.AddPolicy("CorsPolicy", builder =>
                builder.WithOrigins("http://localhost:4200")
                    .WithMethods(verbs)
                    .AllowAnyHeader());
        });

    public static void AddAplicacionServices(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}
