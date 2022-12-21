using FH.CatalogService.Application.Abstraction;
using FH.CatalogService.Application.Abstraction.Services;
using FH.CatalogService.Application.Interfaces;
using FH.CatalogService.Application.Products.Commands.CreateProduct;
using FH.CatalogService.Application.Products.Commands.UpdateProduct;
using FH.CatalogService.Application.Services;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FH.CatalogService.Application.Products.Commands.CreateProduct;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(v => v.Name)
            .MaximumLength(150)
            .NotEmpty();
    }

    private readonly IProductService _ProductService;

    public UpdateProductCommandValidator(IProductService ProductService)
    {
        _ProductService = ProductService;
        

        RuleFor(v => v.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(200).WithMessage("Name must not exceed 150 characters.")
            .MustAsync(BeUniqueName).WithMessage("The specified name already exists.");
    }

    public async Task<bool> BeUniqueName(UpdateProductCommand model, string name, CancellationToken cancellationToken)
    {
        return await _ProductService.CheckProductByName(name,model.Id);
    }


}