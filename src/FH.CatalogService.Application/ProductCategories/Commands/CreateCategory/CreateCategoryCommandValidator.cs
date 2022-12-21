using FH.CatalogService.Application.Abstraction;
using FH.CatalogService.Application.Abstraction.Services;
using FH.CatalogService.Application.ProductCategories.Commands.CreateCategory;
using FH.CatalogService.Application.Services;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FH.CatalogService.Application.ProductCategories.Commands.CreateCategory;

public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(v => v.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(150).WithMessage("Name must not exceed 150 characters.")
            .MustAsync(BeUniqueName).WithMessage("The specified name already exists.");
    }
    private readonly ICategoryService _categoryService;

    public CreateCategoryCommandValidator(ICategoryService categoryService)
    {
        _categoryService = categoryService;


        RuleFor(v => v.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(150).WithMessage("Name must not exceed 150 characters.")
            .MustAsync(BeUniqueName).WithMessage("The specified name already exists.");
    }

    public async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken)
    {
        return await _categoryService.CheckCategoryByName(name);
    }


}