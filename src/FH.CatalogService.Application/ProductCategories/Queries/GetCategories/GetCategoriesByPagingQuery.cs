using AutoMapper;
using FH.CatalogService.Application.Abstraction;
using FH.CatalogService.Application.Abstraction.Repositories;
using FH.CatalogService.Application.Abstraction.Services;
using FH.CatalogService.Application.Dtos;
using FH.CatalogService.Application.Helpers;
using FH.CatalogService.Application.Specifications.Categories;
using FH.CatalogService.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FH.CatalogService.Application.ProductCategories.Queries.GetCategories;

public record GetCategoriesByPagingQuery(CategorySpecPrams SpecPrams) : IRequest<Pagination<CategoryDto>>;

public class GetCategoriesQueryHandller : IRequestHandler<GetCategoriesByPagingQuery, Pagination<CategoryDto>>
{
    private readonly ICategoryService _categoryService;

    public GetCategoriesQueryHandller(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public async Task<Pagination<CategoryDto>> Handle(GetCategoriesByPagingQuery request, CancellationToken cancellationToken)
    {
        var list = await _categoryService.GetCategoryListByPaging(request.SpecPrams);

        return list;
    }
}
