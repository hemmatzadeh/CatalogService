using FH.CatalogService.Application.Abstraction;
using FH.CatalogService.Application.Abstraction.Services;
using FH.CatalogService.Application.Interfaces;
using FH.CatalogService.Application.ProductCategories.Commands;
using FH.CatalogService.Application.Products.Commands.CreateProduct;
using FH.CatalogService.Application.Services;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FH.CatalogService.Application.Products.Commands.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(v => v.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(150).WithMessage("Name must not exceed 200 characters.")
            .MustAsync(BeUniqueName).WithMessage("The specified name already exists.");
    }
    private readonly IProductService _ProductService;

    public CreateProductCommandValidator(IProductService ProductService)
    {
        _ProductService = ProductService;


        RuleFor(v => v.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(150).WithMessage("Name must not exceed 200 characters.")
            .MustAsync(BeUniqueName).WithMessage("The specified name already exists.");
    }

    public async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken)
    {
        return await _ProductService.CheckProductByName(name);
    }


}