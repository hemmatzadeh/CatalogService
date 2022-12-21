using AutoMapper;
using FH.CatalogService.Application.Abstraction;
using FH.CatalogService.Application.Abstraction.Repositories;
using FH.CatalogService.Application.Abstraction.Services;
using FH.CatalogService.Application.Dtos;
using FH.CatalogService.Application.Helpers;
using FH.CatalogService.Application.Specifications;
using FH.CatalogService.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FH.CatalogService.Application.ProductCategories.Queries.GetCategories;

public record GetAllCategoriesQuery() : IRequest<IEnumerable<CategoryDto>>;

public class GetAllCategoriesQueryHandller : IRequestHandler<GetAllCategoriesQuery, IEnumerable<CategoryDto>>
{
    private readonly ICategoryService _categoryService;

    public GetAllCategoriesQueryHandller(ICategoryService categoryService)
    {
        _categoryService = categoryService;

    }

    public async Task<IEnumerable<CategoryDto>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        var list = await _categoryService.GetCategoryList();

        return list;
    }
}
