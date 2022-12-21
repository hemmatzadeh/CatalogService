using FH.CatalogService.Application.Abstraction.Repositories;
using FH.CatalogService.Application.Dtos;
using FH.CatalogService.Application.Specifications;
using FH.CatalogService.Application.Specifications.Categories;
using FH.CatalogService.Domain.Entities;
using FH.CatalogService.Infrastructure.Persistence.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FH.CatalogService.Infrastructure.Repositories
{
    public class ProductCategoryRepository : GenericRepository<ProductCategory>, IProductCategoryRepository
    {
        public ProductCategoryRepository(DatabaseContext Context) : base(Context)
        {
        }

        public async Task<bool> CheckProductCategoryByNameAsync(string name)
        {
            var entity= await GetAsync(x => x.Name.ToLower() == name.ToLower() && x.IsDeleted == false);
            return entity != null;
        }

        public async Task<bool> CheckProductCategoryByNameAsync(string name,int id)
        {
            var entity = await GetAsync(x => x.Id!= id && x.Name.ToLower() == name.ToLower() && x.IsDeleted == false);
            return !entity.Any();
        }

        public async Task<IReadOnlyList<ProductCategory>> GetListOfCategories()
        {

            var list = await GetAsync(x => x.IsDeleted == false);
            return list;
        }

        public async Task<IReadOnlyList<ProductCategory>> GetProductCategoriesWithAttributesAsync()
        {
            var spec = new CategoryWithSpecification();
            var list = await GetAsync(spec);
            return list;
        }

        public async Task<ProductCategory> GetProductCategoryAttributesAsync(int Id)
        {
            var spec = new CategoryAttributesWithSpecification(Id);
            var category = (await GetAsync(spec)).FirstOrDefault();
            return category;
        }

        public async Task<ProductCategory> GetProductCategoryByIdAsync(int Id)
        {
            var category = await GetByIdAsync(Id);
            return category;
        }

        public async Task<IReadOnlyList<ProductCategory>> GetProductCategoryByNameAsync(string name)
        {
            var spec = new CategoryWithSpecification(name);
            return (await GetAsync(spec));
        }
    }
}
