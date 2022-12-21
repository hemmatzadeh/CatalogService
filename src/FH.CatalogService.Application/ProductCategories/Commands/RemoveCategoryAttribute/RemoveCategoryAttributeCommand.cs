using AutoMapper;
using FH.CatalogService.Application.Abstraction;
using FH.CatalogService.Application.Abstraction.Repositories;
using FH.CatalogService.Application.Abstraction.Services;
using FH.CatalogService.Application.ProductCategories.Commands.DeleteCategory;
using FH.CatalogService.Application.Services;
using FH.CatalogService.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FH.CatalogService.Application.ProductCategories.Commands.RemoveCategoryAttribute;

public class RemoveCategoryAttributeCommand : IRequest
{
    public int CategoryId { get; set; }
    public int AttributeId { get; set; }

}

public class RemoveCategoryAttributeCommandHandller : IRequestHandler<RemoveCategoryAttributeCommand>
{
    private readonly ICategoryService _categoryService;

    public RemoveCategoryAttributeCommandHandller(ICategoryService categoryService)
    {
        _categoryService = categoryService;

    }
    public async Task<Unit> Handle(RemoveCategoryAttributeCommand request, CancellationToken cancellationToken)
    {
       await _categoryService.RemoveAttribute(request.CategoryId,request.AttributeId);

        return Unit.Value;

    }
}
