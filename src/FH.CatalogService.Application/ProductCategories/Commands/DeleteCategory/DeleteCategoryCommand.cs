using FH.CatalogService.Application.Abstraction;
using FH.CatalogService.Application.Abstraction.Services;
using FH.CatalogService.Application.Exceptions;
using FH.CatalogService.Application.Services;
using FH.CatalogService.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FH.CatalogService.Application.ProductCategories.Commands.DeleteCategory;
public record DeleteCategoryCommand(int Id) : IRequest;
public class DeleteCategoryCommandHandller : IRequestHandler<DeleteCategoryCommand>
{
    private readonly ICategoryService _categoryService;
    public DeleteCategoryCommandHandller(ICategoryService categoryService)
    {
        _categoryService = categoryService;

    }
    public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        await _categoryService.Delete(request.Id);

        return Unit.Value;
    }
}
