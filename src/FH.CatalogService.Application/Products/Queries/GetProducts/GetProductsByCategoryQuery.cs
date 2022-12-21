using AutoMapper;
using FH.CatalogService.Application.Abstraction;
using FH.CatalogService.Application.Abstraction.Repositories;
using FH.CatalogService.Application.Abstraction.Services;
using FH.CatalogService.Application.Dtos;
using FH.CatalogService.Application.Helpers;
using FH.CatalogService.Application.Interfaces;
using FH.CatalogService.Application.Specifications;
using FH.CatalogService.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FH.CatalogService.Application.Products.Queries.GetProducts;

public record GetProductsByCategoryQuery(int categoryId) : IRequest<IEnumerable<ProductDto>>;

public class GetProductsByCategoryQueryHandller : IRequestHandler<GetProductsByCategoryQuery, IEnumerable<ProductDto>>
{
    private readonly IProductService _productService;

    public GetProductsByCategoryQueryHandller(IProductService productService)
    {
        _productService = productService;

    }

    public async Task<IEnumerable<ProductDto>> Handle(GetProductsByCategoryQuery request, CancellationToken cancellationToken)
    {
        var list = await _productService.GetProductByCategory(request.categoryId);

        return list;
    }
}
