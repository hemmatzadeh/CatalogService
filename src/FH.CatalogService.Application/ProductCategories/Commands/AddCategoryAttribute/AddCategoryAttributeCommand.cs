using AutoMapper;
using FH.CatalogService.Application.Abstraction;
using FH.CatalogService.Application.Abstraction.Repositories;
using FH.CatalogService.Application.Abstraction.Services;
using FH.CatalogService.Application.Services;
using FH.CatalogService.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FH.CatalogService.Application.ProductCategories.Commands.AddCategoryAttribute;

public class AddCategoryAttributeCommand : IRequest<int>
{
    public int CategoryId { get; set; }

    public string Name { get; set; }
}

public class AddCategoryAttributeCommandHandller : IRequestHandler<AddCategoryAttributeCommand, int>
{
    private readonly ICategoryService _categoryService;

    public AddCategoryAttributeCommandHandller(ICategoryService categoryService)
    {
        _categoryService = categoryService;

    }
    public async Task<int> Handle(AddCategoryAttributeCommand request, CancellationToken cancellationToken)
    {
       var entity= await _categoryService.AddAttribute(request.Name, request.CategoryId);

        return entity.Id;
    }
}
