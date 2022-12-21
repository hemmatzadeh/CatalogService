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


namespace FH.CatalogService.Application.ProductCategories.Commands.CreateCategory;

public class CreateCategoryCommand : IRequest<int>
{
    public string Name { get; set; }
    public string? Description { get; set; }
}

public class CreateCategoryCommandHandller : IRequestHandler<CreateCategoryCommand, int>
{
    private readonly ICategoryService _categoryService;

    public CreateCategoryCommandHandller(ICategoryService categoryService)
    {
        _categoryService = categoryService;

    }
    public async Task<int> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var entity = await _categoryService.Create(new Dtos.CategoryDto { Name = request.Name, Description = request.Description });

        return entity.Id;
    }
}
