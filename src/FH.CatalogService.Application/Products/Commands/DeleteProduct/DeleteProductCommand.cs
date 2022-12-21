using FH.CatalogService.Application.Abstraction;
using FH.CatalogService.Application.Abstraction.Services;
using FH.CatalogService.Application.Exceptions;
using FH.CatalogService.Application.Interfaces;
using FH.CatalogService.Application.Services;
using FH.CatalogService.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FH.CatalogService.Application.Products.Commands.DeleteProduct;
public record DeleteProductCommand(int Id) : IRequest;
public class DeleteProductCommandHandller : IRequestHandler<DeleteProductCommand>
{
    private readonly IProductService _ProductService;
    public DeleteProductCommandHandller(IProductService ProductService)
    {
        _ProductService = ProductService;

    }
    public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        await _ProductService.Delete(request.Id);

        return Unit.Value;
    }
}
