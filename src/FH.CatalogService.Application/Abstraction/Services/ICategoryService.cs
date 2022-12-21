using FH.CatalogService.Application.Dtos;
using FH.CatalogService.Application.Helpers;
using FH.CatalogService.Application.Specifications.Categories;
using FH.CatalogService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FH.CatalogService.Application.Abstraction.Services
{
    public interface ICategoryService
    {
        Task<Pagination<CategoryDto>> GetCategoryListByPaging(CategorySpecPrams specPrams);
        Task<IEnumerable<CategoryDto>> GetCategoryList();
        Task<IEnumerable<CategoryDto>> GetCategoryListWithAttributes();
        Task<CategoryDto> GetCategoryById(int categoryId);
        Task<IEnumerable<CategoryDto>> GetCategoryByName(string categoryName);
        Task<bool> CheckCategoryByName(string categoryName);
        Task<bool> CheckCategoryByName(string categoryName,int categoryId);
        Task<CategoryDto> Create(CategoryDto categoryModel);
        Task Update(CategoryDto categoryModel);
        Task Delete(int id);
        Task<CategoryDto> AddAttribute(string attributeName, int categoryId);
        Task RemoveAttribute(int categoryId, int attributeId);

    }
}
