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

public record GetAllProductsWithAttributesQuery() : IRequest<IEnumerable<ProductDto>>;

public class GetAllProductsWithAttributesQueryHandller : IRequestHandler<GetAllProductsWithAttributesQuery, IEnumerable<ProductDto>>
{
    private readonly IProductService _ProductService;

    public GetAllProductsWithAttributesQueryHandller(IProductService ProductService)
    {
        _ProductService = ProductService;

    }

    public async Task<IEnumerable<ProductDto>> Handle(GetAllProductsWithAttributesQuery request, CancellationToken cancellationToken)
    {
        var list = await _ProductService.GetProductListWithAttributes();

        return list;
    }
}
