using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProductsCleanApi.Application.Interfaces;
using ProductsCleanApi.Infrastructure.Persistence;
using ProductsCleanApi.Infrastructure.Repositories;


namespace ProductsCleanApi.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped<IProductRepository, ProductRepository>();

        services.AddScoped<IEmployeeRepository, EmployeeRepository>();

        return services;
    }
}