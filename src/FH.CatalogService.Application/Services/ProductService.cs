using AutoMapper;
using FH.CatalogService.Application.Abstraction.Repositories;
using FH.CatalogService.Application.Dtos;
using FH.CatalogService.Application.Helpers;
using FH.CatalogService.Application.Interfaces;
using FH.CatalogService.Application.Specifications.Categories;
using FH.CatalogService.Application.Specifications.Products;
using FH.CatalogService.Domain.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FH.CatalogService.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IAttributeValueRepository _attributeValueRepository;
        private readonly ILogger<ProductService> _logger;
        private readonly IDistributedCache _distributedCache;
        private readonly IMapper _mapper;
        public ProductService(IProductRepository productRepository, IAttributeValueRepository attributeValueRepository, ILogger<ProductService> logger, IMapper mapper, IDistributedCache distributedCache)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _attributeValueRepository = attributeValueRepository ?? throw new ArgumentNullException(nameof(attributeValueRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _distributedCache = distributedCache ?? throw new ArgumentNullException(nameof(distributedCache));
        }

        public async Task<bool> CheckProductByName(string productName)
        {
            return await _productRepository.CheckProductByNameAsync(productName);
        }

        public async Task<bool> CheckProductByName(string productName, int productId)
        {
            return await _productRepository.CheckProductByNameAsync(productName, productId);
        }

        public async Task<ProductDto> Create(ProductDto productModel)
        {
            await ValidateProductIfExist(productModel);

            var mappedEntity = _mapper.Map<Product>(productModel);

            if (mappedEntity == null)
                throw new ApplicationException($"Entity could not be mapped.");

            var newEntity = await _productRepository.AddAsync(mappedEntity);
            var newMappedEntity = _mapper.Map<ProductDto>(newEntity);
            return newMappedEntity;
        }

        public async Task Delete(int id)
        {
            await ValidateProductIfNotExist(id);

            var entity = await _productRepository.GetByIdAsync(id);
            if (entity == null)
                throw new ApplicationException($"Entity could not be loaded.");

            entity.IsDeleted = true;

            await _productRepository.UpdateAsync(entity);
        }

        public async Task<Pagination<ProductDto>> GetProductListByPaging(ProductSpecPrams specPrams)
        {
            var spec = new ProductWithCategorySpecification(specPrams);
            var total = await _productRepository.CountAsync(spec);
            var list = await _productRepository.GetAsync(spec);
            var data = _mapper.Map<IReadOnlyList<Product>, List<ProductDto>>(list);
            return new Pagination<ProductDto>(specPrams.pageIndex, specPrams.pageSize, total, data);
        }

        public async Task<IEnumerable<ProductDto>> GetProductByCategory(int categoryId)
        {

            _logger.LogInformation("Attempting to get product list from cache");
            var key = $"productListBy_Catd:{categoryId}";
            List<ProductDto> data = new();
            try
            {
                string? catchedMember = await this._distributedCache.GetStringAsync(key);

                if (!string.IsNullOrEmpty(catchedMember))
                {
                    data = JsonConvert.DeserializeObject<List<ProductDto>>(catchedMember, new JsonSerializerSettings
                    {
                        ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
                    });
                    return data;
                }

                var list = await _productRepository.GetAsync(x => x.ProductCategoryId == categoryId && x.IsDeleted == false);
                data = _mapper.Map<IReadOnlyList<Product>, List<ProductDto>>(list);
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

        public async Task<ProductDto> GetProductById(int productId)
        {
            var entity = await _productRepository.GetByIdAsync(productId);
            if (entity == null)
                throw new ApplicationException($"Entity could not be loaded.");

            var data = _mapper.Map<Product, ProductDto>(entity);
            return data;
        }

        public async Task<IEnumerable<ProductDto>> GetProductByName(string productName)
        {
            var list = await _productRepository.GetProductByNameAsync(productName);
            var data = _mapper.Map<IReadOnlyList<Product>, List<ProductDto>>(list);
            return data;
        }

        public async Task<IEnumerable<ProductDto>> GetProductList()
        {
            _logger.LogInformation("Attempting to get product list from cache");
            var key = $"productList";
            List<ProductDto> data = new();
            try
            {
                string? catchedMember = await this._distributedCache.GetStringAsync(key);

                if (!string.IsNullOrEmpty(catchedMember))
                {
                    data = JsonConvert.DeserializeObject<List<ProductDto>>(catchedMember, new JsonSerializerSettings
                    {
                        ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
                    });
                    return data;
                }

                var list = await _productRepository.GetListOfProducts();
                data = _mapper.Map<IReadOnlyList<Product>, List<ProductDto>>(list);
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

        public async Task Update(ProductDto productModel)
        {
            await ValidateProductIfNotExist(productModel.Id);

            var entity = await _productRepository.GetByIdAsync(productModel.Id);
            if (entity == null)
                throw new ApplicationException($"Entity could not be loaded.");

            entity.Name = productModel.Name;
            entity.Price = productModel.Price;

            await _productRepository.UpdateAsync(entity);
        }

        private async Task ValidateProductIfExist(ProductDto categoryModel)
        {
            var existingEntity = await _productRepository.GetByIdAsync(categoryModel.Id);
            if (existingEntity != null)
                throw new ApplicationException($"{categoryModel.ToString()} with this id already exists");
        }

        private async Task ValidateProductIfNotExist(int id)
        {
            var existingEntity = await _productRepository.GetByIdAsync(id);
            if (existingEntity == null)
                throw new ApplicationException($" Category with this id:{id} is not exists");
        }

        public async Task<IEnumerable<ProductDto>> GetProductListWithAttributes()
        {
            var list = await _productRepository.GetProductListWithAttributesAsync();
            var data = _mapper.Map<IReadOnlyList<Product>, List<ProductDto>>(list);
            return data;
        }

        public async Task<ProductDto> GetProductWithAttributesAsync(int id)
        {
            var list = await _productRepository.GetProductWithAttributesAsync(id);
            var data = _mapper.Map<Product, ProductDto>(list);
            return data;
        }

        public async Task<ProductDto> AddAttribute(string attributeValueName, int attributeId, int productId)
        {
            var attribute = await GetExistingOrCreateNewAttributeValue(attributeValueName, attributeId);

            var product = await _productRepository.GetProductWithAttributesAsync(productId);

            product.ProductAttributeValues.Add(new ProductAttributeValues { ProductId = product.Id, AttributeValueId = attribute.Id });

            await _productRepository.UpdateAsync(product);

            var mappedEntity = _mapper.Map<ProductDto>(product);

            return mappedEntity;
        }

        public async Task RemoveAttribute(int productId, int attributeId)
        {
            var spec = new ProductWithCategorySpecification(productId);
            var product = (await _productRepository.GetAsync(spec)).FirstOrDefault();

            if (product == null)
                throw new ApplicationException($" Category with this id:{productId} is not exists");

            var removedItem = product.ProductAttributeValues.FirstOrDefault(x => x.AttributeValueId == attributeId);
            if (removedItem != null)
            {
                product.ProductAttributeValues.Remove(removedItem);
            }

            await _productRepository.UpdateAsync(product);
        }

        private async Task<AttributeValue> GetExistingOrCreateNewAttributeValue(string valueName,int attributeId)
        {
            var attribute = await _attributeValueRepository.GetByAttributeValueNameAsync(valueName);
            if (attribute != null)
                return attribute;

            // if it is first attempt create new
            var newAttribute = new AttributeValue
            {
                AttributeId= attributeId,
                ValueName = valueName
            };

            await _attributeValueRepository.AddAsync(newAttribute);
            return newAttribute;
        }
    }
}
