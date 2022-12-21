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


namespace FH.CatalogService.Application.Products.Commands.RemoveProductAttribute;

public class RemoveProductAttributeCommand : IRequest
{
    public int ProductId { get; set; }
    public int AttributeId { get; set; }

}

public class RemoveProductAttributeCommandHandller : IRequestHandler<RemoveProductAttributeCommand>
{
    private readonly IProductService _ProductService;

    public RemoveProductAttributeCommandHandller(IProductService ProductService)
    {
        _ProductService = ProductService;

    }
    public async Task<Unit> Handle(RemoveProductAttributeCommand request, CancellationToken cancellationToken)
    {
        await _ProductService.RemoveAttribute(request.ProductId,request.AttributeId);

        return Unit.Value;


    }
}
