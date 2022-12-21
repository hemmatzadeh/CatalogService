using FH.CatalogService.Application.Specifications.Base;
using FH.CatalogService.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace FH.CatalogService.Application.Specifications.Categories;

public class CategoryWithSpecification : BaseSpecification<ProductCategory>
{
    public CategoryWithSpecification(string categoryName)
        : base(p => p.Name.ToLower().Contains(categoryName.ToLower()) && p.IsDeleted == false)
    {
        AddInclude("ProductCategoryAttributes");
        AddInclude("ProductCategoryAttributes.ProductAttribute");

    }

    public CategoryWithSpecification(int categoryId)
        : base(p => p.Id == categoryId)
    {
        AddInclude("ProductCategoryAttributes");
        AddInclude("ProductCategoryAttributes.ProductAttribute");
    }

    public CategoryWithSpecification()
        : base(p => p.IsDeleted == false)
    {
        AddInclude("ProductCategoryAttributes");
        AddInclude("ProductCategoryAttributes.ProductAttribute");
    }

    public CategoryWithSpecification(CategorySpecPrams categorySpecPrams)
       : base
       (x =>
       (string.IsNullOrEmpty(categorySpecPrams.Search) || x.Name.Contains(categorySpecPrams.Search) || x.ProductCategoryAttributes.Any(c => c.ProductAttribute.Name.Contains(categorySpecPrams.Search))) && !x.IsDeleted)
    {
        AddInclude("ProductCategoryAttributes");
        AddInclude("ProductCategoryAttributes.ProductAttribute");
        AddOrderBy(x => x.Name);
        ApplyPagging(categorySpecPrams.pageSize * categorySpecPrams.pageIndex, categorySpecPrams.pageSize);
    }
}
