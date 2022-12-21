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
    public class ProductCategoryAttributeConfiguration : IEntityTypeConfiguration<ProductCategoryAttribute>
    {
        public void Configure(EntityTypeBuilder<ProductCategoryAttribute> builder)
        {
            builder.HasKey(s => new { s.CategoryId, s.AttributeId });

            builder.HasOne(ss => ss.ProductCategory)
                .WithMany(s => s.ProductCategoryAttributes)
                .HasForeignKey(ss => ss.CategoryId);

            builder.HasOne(ss => ss.ProductAttribute)
                .WithMany(s => s.ProductCategoryAttributes)
                .HasForeignKey(ss => ss.AttributeId);
        }
    }
}
