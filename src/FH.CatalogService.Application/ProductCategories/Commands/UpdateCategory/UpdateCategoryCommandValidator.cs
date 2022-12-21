using FH.CatalogService.Application.Abstraction;
using FH.CatalogService.Application.Abstraction.Services;
using FH.CatalogService.Application.ProductCategories.Commands.CreateCategory;
using FH.CatalogService.Application.ProductCategories.Commands.UpdateCategory;
using FH.CatalogService.Application.Services;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FH.CatalogService.Application.ProductCategories.Commands.CreateCategory;

public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidator()
    {
        RuleFor(v => v.Name)
            .MaximumLength(150)
            .NotEmpty();
    }

    private readonly ICategoryService _categoryService;

    public UpdateCategoryCommandValidator(ICategoryService categoryService)
    {
        _categoryService = categoryService;
        

        RuleFor(v => v.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(150).WithMessage("Name must not exceed 150 characters.")
            .MustAsync(BeUniqueName).WithMessage("The specified name already exists.");
    }

    public async Task<bool> BeUniqueName(UpdateCategoryCommand model, string name, CancellationToken cancellationToken)
    {
        return await _categoryService.CheckCategoryByName(name,model.Id);
    }


}