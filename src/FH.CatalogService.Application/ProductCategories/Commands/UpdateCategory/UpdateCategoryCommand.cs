using FH.CatalogService.Application.Abstraction;
using FH.CatalogService.Application.Abstraction.Services;
using FH.CatalogService.Application.Exceptions;
using FH.CatalogService.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FH.CatalogService.Application.ProductCategories.Commands.UpdateCategory;

public class UpdateCategoryCommand : IRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
}

public class UpdateCategoryCommandHandller : IRequestHandler<UpdateCategoryCommand>
{
    private readonly ICategoryService _categoryService;

    public UpdateCategoryCommandHandller(ICategoryService categoryService)
    {
        _categoryService = categoryService;

    }

    public async Task<Unit> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        await _categoryService.Update(new Dtos.CategoryDto { Id = request.Id, Name = request.Name, Description = request.Description });

        return Unit.Value;
    }
}
