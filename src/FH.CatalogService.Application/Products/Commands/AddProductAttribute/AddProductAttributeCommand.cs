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


namespace FH.CatalogService.Application.Products.Commands.AddProductAttribute;

public class AddProductAttributeCommand : IRequest<int>
{
    public int ProductId { get; set; }
    public int AttributeId { get; set; }
    public string ValueName { get; set; }
}

public class AddProductAttributeCommandHandller : IRequestHandler<AddProductAttributeCommand, int>
{
    private readonly IProductService _ProductService;

    public AddProductAttributeCommandHandller(IProductService ProductService)
    {
        _ProductService = ProductService;

    }
    public async Task<int> Handle(AddProductAttributeCommand request, CancellationToken cancellationToken)
    {
        var entity= await _ProductService.AddAttribute(request.ValueName,request.AttributeId, request.ProductId);

        return entity.Id;
    }
}
