using AutoMapper;
using FH.CatalogService.Application.Abstraction;
using FH.CatalogService.Application.Abstraction.Repositories;
using FH.CatalogService.Application.Abstraction.Services;
using FH.CatalogService.Application.Dtos;
using FH.CatalogService.Application.Helpers;
using FH.CatalogService.Application.Interfaces;
using FH.CatalogService.Application.Specifications.Products;
using FH.CatalogService.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FH.CatalogService.Application.Products.Queries.GetProducts;

public record GetProductsByPagingQuery(ProductSpecPrams SpecPrams) : IRequest<Pagination<ProductDto>>;

public class GetProductsQueryHandller : IRequestHandler<GetProductsByPagingQuery, Pagination<ProductDto>>
{
    private readonly IProductService _ProductService;

    public GetProductsQueryHandller(IProductService ProductService)
    {
        _ProductService = ProductService;
    }

    public async Task<Pagination<ProductDto>> Handle(GetProductsByPagingQuery request, CancellationToken cancellationToken)
    {
        var list = await _ProductService.GetProductListByPaging(request.SpecPrams);

        return list;
    }
}
