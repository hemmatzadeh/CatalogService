using FH.CatalogService.Application.Abstraction.Repositories.Base;
using FH.CatalogService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FH.CatalogService.Application.Abstraction.Repositories
{
    public interface IProductCategoryRepository : IGenericRepository<ProductCategory>
    {
        Task<ProductCategory> GetProductCategoryByIdAsync(int Id);
        Task<bool> CheckProductCategoryByNameAsync(string name);
        Task<bool> CheckProductCategoryByNameAsync(string name, int id);
        Task<IReadOnlyList<ProductCategory>> GetListOfCategories();
        Task<ProductCategory> GetProductCategoryAttributesAsync(int Id);
        Task<IReadOnlyList<ProductCategory>> GetProductCategoriesWithAttributesAsync();
        Task<IReadOnlyList<ProductCategory>> GetProductCategoryByNameAsync(string name);



    }
}
