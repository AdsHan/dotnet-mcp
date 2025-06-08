using Microsoft.EntityFrameworkCore;
using ModelContextProtocol.API.Application.Services;
using ModelContextProtocol.API.Data;
using ModelContextProtocol.API.Data.Repositories;

namespace ModelContextProtocol.API.Configuration;

public static class DependencyInjectionConfig
{
    public static IServiceCollection AddDependencyConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<CatalogDbContext>(options => options.UseInMemoryDatabase("CatalogDB"));

        services.AddScoped<IProductRepository, ProductRepository>();

        services.AddTransient<ProductPopulateService>();

        return services;
    }
}
