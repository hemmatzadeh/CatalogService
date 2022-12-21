using FH.CatalogService.Application.Abstraction.Repositories.Base;
using FH.CatalogService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FH.CatalogService.Application.Abstraction.Repositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<Product> GetProductByIdAsync(int id);
        Task<IReadOnlyList<Product>> GetProductByNameAsync(string name);
        Task<IReadOnlyList<Product>> GetListOfProducts();
        Task<bool> CheckProductByNameAsync(string name);
        Task<bool> CheckProductByNameAsync(string name, int id);
        Task<IReadOnlyList<Product>> GetProductByCategoryAsync(int categoryId);
        Task<IReadOnlyList<Product>> GetProductListWithAttributesAsync();
        Task<Product> GetProductWithAttributesAsync(int id);

    }
}
