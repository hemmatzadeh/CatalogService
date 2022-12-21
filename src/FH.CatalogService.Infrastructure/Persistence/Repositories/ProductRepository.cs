using FH.CatalogService.Application.Abstraction.Repositories;
using FH.CatalogService.Application.Dtos;
using FH.CatalogService.Application.Specifications;
using FH.CatalogService.Application.Specifications.Categories;
using FH.CatalogService.Application.Specifications.Products;
using FH.CatalogService.Domain.Entities;
using FH.CatalogService.Infrastructure.Persistence.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FH.CatalogService.Infrastructure.Repositories
{
    internal class ProductRepository : GenericRepository<Product>, IProductRepository
    {

        public ProductRepository(DatabaseContext Context) : base(Context)
        {
        }

        public async Task<bool> CheckProductByNameAsync(string name)
        {
            var entity = await GetAsync(x => x.Name.ToLower() == name.ToLower() && x.IsDeleted == false);
            return entity != null;
        }

        public async Task<bool> CheckProductByNameAsync(string name, int id)
        {
            var entity = await GetAsync(x => x.Id != id && x.Name.ToLower() == name.ToLower() && x.IsDeleted == false);
            return !entity.Any();
        }

        public async Task<IReadOnlyList<Product>> GetListOfProducts()
        {
            var list = await GetAllAsync();
            return list;
        }

        public async Task<Product> GetProductWithAttributesAsync(int Id)
        {
            var spec = new ProductWithCategorySpecification(Id);
            var product = (await GetAsync(spec)).FirstOrDefault();
            return product;
        }

        public async Task<IReadOnlyList<Product>> GetProductListWithAttributesAsync()
        {
            var spec = new ProductWithCategorySpecification();
            var list = (await GetAsync(spec));
            return list;
        }

        public async Task<IReadOnlyList<Product>> GetProductByCategoryAsync(int categoryId)
        {
            var list = await GetAsync(x => x.ProductCategoryId == categoryId && x.IsDeleted == false);
            return list;
        }

        public async Task<Product> GetProductByIdAsync(int Id)
        {
            var product = await GetByIdAsync(Id);
            return product;
        }

        public async Task<IReadOnlyList<Product>> GetProductByNameAsync(string name)
        {
            var spec = new ProductWithCategorySpecification(name);
            return (await GetAsync(spec));
        }
    }
}
