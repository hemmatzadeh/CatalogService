using FH.CatalogService.Application.Abstraction;
using FH.CatalogService.Application.Abstraction.Repositories;
using FH.CatalogService.Application.Abstraction.Repositories.Base;
using FH.CatalogService.Infrastructure.Persistence.Repositories;
using FH.CatalogService.Infrastructure.Persistence.Repositories.Base;
using FH.CatalogService.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FH.CatalogService.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Default");
            services.AddDbContext<IDatabseContext, DatabaseContext>(options =>
                            options.UseSqlServer(connectionString, x =>
                            x.MigrationsAssembly(typeof(DatabaseContext).Namespace)));

            services.AddScoped<DatabaseContextInitialiser>();
            
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductAttributeRepository, ProductAttributeRepository>();
            services.AddScoped<IAttributeValueRepository, AttributeValueRepository>();


            return services;
        }
    }
}