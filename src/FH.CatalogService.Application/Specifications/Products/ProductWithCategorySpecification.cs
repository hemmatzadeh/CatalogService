using FH.CatalogService.Application.Specifications.Base;
using FH.CatalogService.Application.Specifications.Categories;
using FH.CatalogService.Domain.Entities;

namespace FH.CatalogService.Application.Specifications.Products;

public class ProductWithCategorySpecification : BaseSpecification<Product>
{
    public ProductWithCategorySpecification(string productName)
        : base(p => p.Name.ToLower().Contains(productName.ToLower()) && p.IsDeleted == false)
    {
        AddInclude("ProductCategory");
        AddInclude("ProductAttributeValues");
        AddInclude("ProductAttributeValues.AttributeValue");
        AddInclude("ProductAttributeValues.AttributeValue.ProductAttribute");

    }

    public ProductWithCategorySpecification(int productId)
        : base(p => p.Id == productId)
    {
        AddInclude("ProductCategory");
        AddInclude("ProductAttributeValues");
        AddInclude("ProductAttributeValues.AttributeValue");
        AddInclude("ProductAttributeValues.AttributeValue.ProductAttribute");
    }

    public ProductWithCategorySpecification()
        : base(p=>p.IsDeleted == false)
    {
        AddInclude("ProductCategory");
        AddInclude("ProductAttributeValues");
        AddInclude("ProductAttributeValues.AttributeValue");
        AddInclude("ProductAttributeValues.AttributeValue.ProductAttribute");
    }

    public ProductWithCategorySpecification(ProductSpecPrams productSpecPrams)
      : base
      (x =>
      (string.IsNullOrEmpty(productSpecPrams.Search) 
      || x.Name.Contains(productSpecPrams.Search) 
      || x.ProductAttributeValues.Any(c => c.AttributeValue.ValueName.Contains(productSpecPrams.Search)) 
      || x.ProductAttributeValues.Any(c => c.AttributeValue.ProductAttribute.Name.Contains(productSpecPrams.Search))) 
      && (productSpecPrams.minPrice == null || x.Price>=productSpecPrams.minPrice) && (productSpecPrams.maxPrice == null || x.Price <= productSpecPrams.maxPrice)
      && !x.IsDeleted)
    {
        AddInclude("ProductCategory");
        AddInclude("ProductAttributeValues");
        AddInclude("ProductAttributeValues.AttributeValue");
        AddInclude("ProductAttributeValues.AttributeValue.ProductAttribute");
        AddOrderBy(x => x.Name);
        
        ApplyPagging(productSpecPrams.pageSize * productSpecPrams.pageIndex, productSpecPrams.pageSize);

    }
}
