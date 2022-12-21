using FH.CatalogService.Application.Abstraction.Repositories;
using FH.CatalogService.Application.Abstraction.Services;
using FH.CatalogService.Application.Services;
using FH.CatalogService.Domain.Entities;
using FH.CatalogService.Infrastructure;
using FH.CatalogService.Infrastructure.Persistence.Repositories;
using FH.CatalogService.Infrastructure.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Moq;
using NF.ExchangeRates.Core;
using System.Text;

namespace FH.CatalogService.Tests
{
    public class CategoryServiceTests
    {
        private readonly Mock<ILogger<CategoryService>> _logger = new Mock<ILogger<CategoryService>>();
        private readonly Mock<IDistributedCache> _cache = new Mock<IDistributedCache>();
        private readonly Mock<IProductCategoryRepository> _pcrMock;
        private readonly IServiceProvider _provider;

        private readonly DatabaseContext _context;
        private readonly IProductAttributeRepository _productCategoryAttribute;
        public CategoryServiceTests()
        {
            var json = "{\r\n  \"ConnectionStrings\": {\r\n    \"Default\": \"Server=catalogdb;Database=CatalogService;uid=sa;pwd=yl!@AfZAjA95\"\r\n  },\r\n  \"Logging\": {\r\n    \"LogLevel\": {\r\n      \"Default\": \"Information\",\r\n      \"Microsoft.AspNetCore\": \"Warning\"\r\n    }\r\n  },\r\n  \"AllowedHosts\": \"*\"\r\n}\r\n";
            var config = new ConfigurationBuilder()
                .AddJsonStream(new MemoryStream(Encoding.UTF8.GetBytes(json)))
                .Build();

            var _dbOptions = new DbContextOptionsBuilder<DatabaseContext>()
               .UseInMemoryDatabase("CatalogService_test")
               .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
               .Options;

            _context = new DatabaseContext(_dbOptions);

            _pcrMock = new Mock<IProductCategoryRepository>();

            _productCategoryAttribute = new ProductAttributeRepository(_context);

            _provider = new ServiceCollection()
                .AddInfrastructureServices(config)
                .AddApplicatrionServices()
                .AddTransient(x => _pcrMock.Object)
                .Replace(new ServiceDescriptor(typeof(ILogger<CategoryService>), _logger.Object))
                .Replace(new ServiceDescriptor(typeof(IDistributedCache), _cache.Object))

                .Replace(new ServiceDescriptor(typeof(IProductCategoryRepository), _pcrMock.Object))
                .Replace(new ServiceDescriptor(typeof(IProductAttributeRepository), _productCategoryAttribute))

                .BuildServiceProvider();

            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
        }

        [Fact]
        public async Task AddAttributeShouldThrowExceptionWhenCategoryDoesNotExists()
        {
            var scope = _provider.CreateScope();
            var categoryService = scope.ServiceProvider.GetRequiredService<ICategoryService>();
            categoryService.Should().NotBeNull();

            _ = categoryService.Invoking(o => o.AddAttribute("aName", 1))
            .Should().ThrowAsync<Exception>();

        }

        [Fact]
        public async Task AddAttributeShouldAdd()
        {
            var scope = _provider.CreateScope();
            var categoryService = scope.ServiceProvider.GetRequiredService<ICategoryService>();
            categoryService.Should().NotBeNull();
            _pcrMock.Setup(u => u.GetProductCategoryAttributesAsync(It.IsAny<int>()))
                .ReturnsAsync(new Domain.Entities.ProductCategory() { Id = 1, Name = "test" });
            var x = await categoryService.AddAttribute("aName", 1);
            x.Should().NotBeNull();

        }

        [Fact]
        public async Task AddCategoryShouldAdd()
        {
            var scope = _provider.CreateScope();
            var categoryService = scope.ServiceProvider.GetRequiredService<ICategoryService>();
            categoryService.Should().NotBeNull();
            _pcrMock.Setup(x => x.AddAsync(It.IsAny<ProductCategory>()))
                .ReturnsAsync(new ProductCategory() { Id = 1 });
            var x = await categoryService.Create(new Application.Dtos.CategoryDto { Id = 1, Name = "Testcategory" });
            x.Should().NotBeNull();

        }

    }
}
