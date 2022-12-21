using FH.CatalogService.Application.Specifications.Base;
using FH.CatalogService.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace FH.CatalogService.Application.Specifications.Categories;

public class CategoryAttributesWithSpecification : BaseSpecification<ProductCategory>
{
    public CategoryAttributesWithSpecification(int categoryId)
        : base(p => p.Id == categoryId && p.IsDeleted == false)
    {
        AddInclude("ProductCategoryAttributes");
        AddInclude("ProductCategoryAttributes.ProductAttribute");


    }
}
