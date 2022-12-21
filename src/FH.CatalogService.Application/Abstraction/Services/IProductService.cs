using FH.CatalogService.Application.Dtos;
using FH.CatalogService.Application.Helpers;
using FH.CatalogService.Application.Specifications.Categories;
using FH.CatalogService.Application.Specifications.Products;
using FH.CatalogService.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FH.CatalogService.Application.Interfaces
{
    public interface IProductService
    {
        Task<Pagination<ProductDto>> GetProductListByPaging(ProductSpecPrams specPrams);
        Task<IEnumerable<ProductDto>> GetProductList();
        Task<IEnumerable<ProductDto>> GetProductListWithAttributes();
        Task<ProductDto> GetProductWithAttributesAsync(int Id);
        Task<ProductDto> GetProductById(int productId);
        Task<bool> CheckProductByName(string productName);
        Task<bool> CheckProductByName(string productName, int productId);
        Task<IEnumerable<ProductDto>> GetProductByName(string productName);
        Task<IEnumerable<ProductDto>> GetProductByCategory(int categoryId);
        Task<ProductDto> Create(ProductDto productModel);
        Task Update(ProductDto productModel);
        Task Delete(int id);
        Task<ProductDto> AddAttribute(string attributeValueName,int attributeId, int productId);
        Task RemoveAttribute(int productId, int attributeId);
    }
}
