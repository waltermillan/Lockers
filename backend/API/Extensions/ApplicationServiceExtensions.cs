using API.Services;
using Core.Interfaces;
using Infrastructure.Helpers;
using Infrastructure.Repositories;
using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using System.Reflection;

namespace API.Extensions;
public static class ApplicationServiceExtensions
{
    public static void ConfigureCors(this IServiceCollection services, IConfiguration configuration) =>
        services.AddCors(options =>
        {
            string[] verbs = configuration.GetSection("CorsSettings:Methods").Get<string[]>();

            var origins = configuration.GetSection("CorsSettings:Origins").Get<string[]>();
            var policyName = configuration.GetSection("CorsSettings:PolicyName").Get<string>();

            options.AddPolicy(policyName, builder =>
                builder.WithOrigins(origins)  // Allows only these developing origins
                    .WithMethods(verbs)
                    .AllowAnyHeader());
        });

    public static void AddAplicacionServices(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddAutoMapper(Assembly.GetEntryAssembly());
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IDocumentRepository, DocumentRepository>();
        services.AddScoped<ILocationRepository, LocationRepository>();
        services.AddScoped<ILockerRepository, LockerRepository>();
        services.AddScoped<IPriceRepository, PriceRepository>();
        services.AddScoped<IRentRepository, RentRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IPasswordHasher, BcryptPasswordHasher>();
        services.AddScoped<LockerDTOService>();
        services.AddScoped<CustomerDTOService>();
        services.AddScoped<RentDTOService>();
        services.AddScoped<UserDTOService>();

    }
}
