using FH.CatalogService.Application.Abstraction.Repositories;
using FH.CatalogService.Domain.Entities;
using FH.CatalogService.Infrastructure;
using FH.CatalogService.Infrastructure.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace FH.CatalogService.Tests
{

    public class RepositoryTests
    {
        private readonly DatabaseContext _context;

        public RepositoryTests()
        {
            var _dbOptions = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase("CatalogService_test")
                .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            _context = new DatabaseContext(_dbOptions);
        }

        [Fact]
        public async Task GetListOfProductsShouldReturnEmptyListWhenDatabaseIsEmpty()
        {
            prepaireDatabase();

            var prodRep = new ProductRepository(_context);

            var productList =await prodRep.GetListOfProducts();

            productList.Should().NotBeNull();
            productList.Count.Should().Be(0);
        }

        [Fact]
        public async Task AddProductShouldAdd1RecordToProductList()
        {
            prepaireDatabase();
            var prodRep = new ProductRepository(_context);
            var newProduct = new Product()
            {
                Id = 1,
                IsDeleted = false,
                Name = "sample product",
                Price = 100
            };

            var addedProduct = await prodRep.AddAsync(newProduct);

            var productList = await prodRep.GetListOfProducts();

            productList.Should().NotBeNull();
            productList.Count.Should().Be(1);

            var p = productList.First();
            p.Should().NotBeNull();

            p.Id.Should().Be(addedProduct.Id);
            p.Name.Should().Be(addedProduct.Name);
        }

        private void prepaireDatabase()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
        }
    }
}
