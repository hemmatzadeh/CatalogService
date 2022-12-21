using FH.CatalogService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FH.CatalogService.Infrastructure.Configurations
{
    public class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.Property(c => c.Name).HasMaxLength(150).IsRequired();
            builder.Property(c => c.Description).HasMaxLength(200);

            builder.HasMany(e => e.Products)
                    .WithOne(s => s.ProductCategory)
                    .HasForeignKey(s => s.ProductCategoryId)
                    .OnDelete(DeleteBehavior.Restrict);


        }
    }
}
