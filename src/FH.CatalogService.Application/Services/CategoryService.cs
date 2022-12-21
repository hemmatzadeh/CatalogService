using AutoMapper;
using AutoMapper.Internal.Mappers;
using FH.CatalogService.Application.Abstraction.Repositories;
using FH.CatalogService.Application.Abstraction.Services;
using FH.CatalogService.Application.Dtos;
using FH.CatalogService.Application.Helpers;
using FH.CatalogService.Application.Specifications;
using FH.CatalogService.Application.Specifications.Categories;
using FH.CatalogService.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FH.CatalogService.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IProductCategoryRepository _categoryRepository;
        private readonly IProductAttributeRepository _productCategoryAttribute;
        private readonly ILogger<CategoryService> _logger;
        private readonly IDistributedCache _distributedCache;
        private readonly IMapper _mapper;

        public CategoryService(IProductCategoryRepository categoryRepository, IProductAttributeRepository productCategoryAttribute, ILogger<CategoryService> logger, IMapper mapper, IDistributedCache distributedCache)
        {
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
            _productCategoryAttribute = productCategoryAttribute ?? throw new ArgumentNullException(nameof(productCategoryAttribute));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _distributedCache = distributedCache ?? throw new ArgumentNullException(nameof(distributedCache));
        }

        public async Task<CategoryDto> Create(CategoryDto categoryModel)
        {
            await ValidateCategoryIfExist(categoryModel);

            var mappedEntity = _mapper.Map<ProductCategory>(categoryModel);

            if (mappedEntity == null)
                throw new ApplicationException($"Entity could not be mapped.");

            var newEntity = await _categoryRepository.AddAsync(mappedEntity);
            var newMappedEntity = _mapper.Map<CategoryDto>(newEntity);
            return newMappedEntity;
        }

        public async Task Delete(int id)
        {
            await ValidateCategoryIfNotExist(id);

            var entity = await _categoryRepository.GetByIdAsync(id);
            if (entity == null)
                throw new ApplicationException($"Entity could not be loaded.");

            entity.IsDeleted = true;

            await _categoryRepository.UpdateAsync(entity);
        }

        public async Task<CategoryDto> GetCategoryById(int categoryId)
        {
            var entity = await _categoryRepository.GetByIdAsync(categoryId);
            if (entity == null)
                throw new ApplicationException($"Entity could not be loaded.");

            var data = _mapper.Map<ProductCategory, CategoryDto>(entity);
            return data;
        }

        public async Task<IEnumerable<CategoryDto>> GetCategoryByName(string categoryName)
        {
            var list = await _categoryRepository.GetProductCategoryByNameAsync(categoryName);
            var data = _mapper.Map<IReadOnlyList<ProductCategory>, List<CategoryDto>>(list);
            return data;
        }

        public async Task<Pagination<CategoryDto>> GetCategoryListByPaging(CategorySpecPrams specPrams)
        {
            var spec = new CategoryWithSpecification(specPrams);
            var total = await _categoryRepository.CountAsync(spec);
            var list = await _categoryRepository.GetAsync(spec);
            var data = _mapper.Map<IReadOnlyList<ProductCategory>, List<CategoryDto>>(list);
            return new Pagination<CategoryDto>(specPrams.pageIndex, specPrams.pageSize, total, data);
        }

        public async Task<IEnumerable<CategoryDto>> GetCategoryList()
        {
            _logger.LogInformation("Attempting to get category list from cache");
            var key = $"categoryList";
            List<CategoryDto> data = new();
            try
            {
                string? catchedMember = await this._distributedCache.GetStringAsync(key);

                if (!string.IsNullOrEmpty(catchedMember))
                {
                    data = JsonConvert.DeserializeObject<List<CategoryDto>>(catchedMember, new JsonSerializerSettings
                    {
                        ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
                    });
                    return data;
                }

                var list = await _categoryRepository.GetListOfCategories();
                data = _mapper.Map<IReadOnlyList<ProductCategory>, List<CategoryDto>>(list);
                if (data != null)
                {
                    await this._distributedCache.SetStringAsync(key, JsonConvert.SerializeObject(data));
                    return data;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Read from database Error: {0}", ex);
            }
            return data;
        }

        public async Task Update(CategoryDto categoryModel)
        {
            await ValidateCategoryIfNotExist(categoryModel.Id);

            var entity = await _categoryRepository.GetByIdAsync(categoryModel.Id);
            if (entity == null)
                throw new ApplicationException($"Entity could not be loaded.");

            entity.Name = categoryModel.Name;
            entity.Description = categoryModel.Description;

            await _categoryRepository.UpdateAsync(entity);
        }

        public async Task<IEnumerable<CategoryDto>> GetCategoryListWithAttributes()
        {
            var list = await _categoryRepository.GetProductCategoriesWithAttributesAsync();
            var data = _mapper.Map<IReadOnlyList<ProductCategory>, List<CategoryDto>>(list);
            return data;
        }

        public async Task<bool> CheckCategoryByName(string categoryName)
        {
            return await _categoryRepository.CheckProductCategoryByNameAsync(categoryName);
        }

        public async Task<bool> CheckCategoryByName(string categoryName, int categoryId)
        {
            return await _categoryRepository.CheckProductCategoryByNameAsync(categoryName, categoryId);
        }

        public async Task<CategoryDto> AddAttribute(string attributeName, int categoryId)
        {
            var attribute = await GetExistingOrCreateNewAttribute(attributeName);

            var category = await _categoryRepository.GetProductCategoryAttributesAsync(categoryId);

            category.ProductCategoryAttributes.Add(new ProductCategoryAttribute { CategoryId = categoryId, AttributeId = attribute.Id });

            await _categoryRepository.UpdateAsync(category);

            var mappedEntity = _mapper.Map<CategoryDto>(category);

            return mappedEntity;
        }

        public async Task RemoveAttribute(int categoryId, int attributeId)
        {
            var spec = new CategoryWithSpecification(categoryId);
            var category = (await _categoryRepository.GetAsync(spec)).FirstOrDefault();

            if (category == null)
                throw new ApplicationException($" Category with this id:{categoryId} is not exists");

            var removedItem = category.ProductCategoryAttributes.FirstOrDefault(x => x.AttributeId == attributeId);
            if (removedItem != null)
            {
                category.ProductCategoryAttributes.Remove(removedItem);
            }

            await _categoryRepository.UpdateAsync(category);
        }

        private async Task ValidateCategoryIfExist(CategoryDto categoryModel)
        {
            var existingEntity = await _categoryRepository.GetByIdAsync(categoryModel.Id);
            if (existingEntity != null)
                throw new ApplicationException($"{categoryModel.ToString()} with this id already exists");
        }

        private async Task ValidateCategoryIfNotExist(int id)
        {
            var existingEntity = await _categoryRepository.GetByIdAsync(id);
            if (existingEntity == null)
                throw new ApplicationException($" Category with this id:{id} is not exists");
        }

        private async Task<ProductAttribute> GetExistingOrCreateNewAttribute(string name)
        {
            var attribute = await _productCategoryAttribute.GetByAttributeNameAsync(name);
            if (attribute != null)
                return attribute;

            // if it is first attempt create new
            var newAttribute = new ProductAttribute
            {
                Name = name
            };

            await _productCategoryAttribute.AddAsync(newAttribute);
            return newAttribute;
        }
    }
}
