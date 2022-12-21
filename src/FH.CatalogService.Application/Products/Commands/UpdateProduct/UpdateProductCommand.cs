using FH.CatalogService.Application.Abstraction;
using FH.CatalogService.Application.Abstraction.Services;
using FH.CatalogService.Application.Exceptions;
using FH.CatalogService.Application.Interfaces;
using FH.CatalogService.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FH.CatalogService.Application.Products.Commands.UpdateProduct;

public class UpdateProductCommand : IRequest
{
    public int Id { get; set; }

    public string Name { get; set; }

    public double Price { get; set; }

    public int CategoryId { get; set; }
}

public class UpdateProductCommandHandller : IRequestHandler<UpdateProductCommand>
{
    private readonly IProductService _ProductService;

    public UpdateProductCommandHandller(IProductService ProductService)
    {
        _ProductService = ProductService;

    }

    public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        await _ProductService.Update(new Dtos.ProductDto { Id = request.Id, Name = request.Name, CategotyId = request.CategoryId,Price=request.Price });

        return Unit.Value;
    }
}
