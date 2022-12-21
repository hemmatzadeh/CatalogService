using FH.CatalogService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FH.CatalogService.Application.Abstraction
{
    public interface IDatabseContext
    {
        DbSet<Product> Products { get; }

        DbSet<ProductCategory> ProductCategories { get; }

        DbSet<ProductAttribute> Attributes { get; }

        DbSet<ProductCategoryAttribute> CategoryAttributes { get; }

        DbSet<AttributeValue> AttributeValues { get; }

        DbSet<ProductAttributeValues> ProductAttributeValues { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    }
}
