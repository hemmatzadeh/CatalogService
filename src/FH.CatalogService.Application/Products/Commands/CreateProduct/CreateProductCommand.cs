using AutoMapper;
using FH.CatalogService.Application.Abstraction;
using FH.CatalogService.Application.Abstraction.Repositories;
using FH.CatalogService.Application.Abstraction.Services;
using FH.CatalogService.Application.Interfaces;
using FH.CatalogService.Application.Services;
using FH.CatalogService.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FH.CatalogService.Application.Products.Commands.CreateProduct;

public class CreateProductCommand : IRequest<int>
{
    public string Name { get; set; }
    public double Price { get; set; }
    public int ProductCategoryId { get; set; }
}

public class CreateProductCommandHandller : IRequestHandler<CreateProductCommand, int>
{
    private readonly IProductService _productService;

    public CreateProductCommandHandller(IProductService productService)
    {
        _productService = productService;

    }
    public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var entity = await _productService.Create(new Dtos.ProductDto { Name = request.Name, CategotyId = request.ProductCategoryId, Price=request.Price });

        return entity.Id;
    }
}
