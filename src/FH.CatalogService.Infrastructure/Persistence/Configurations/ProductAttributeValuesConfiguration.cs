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
    public class ProductAttributeValuesConfiguration : IEntityTypeConfiguration<ProductAttributeValues>
    {
        public void Configure(EntityTypeBuilder<ProductAttributeValues> builder)
        {
            
            builder.HasKey(x => new { x.ProductId, x.AttributeValueId } );

            builder.HasOne(ss => ss.Product)
                .WithMany(s => s.ProductAttributeValues)
                .HasForeignKey(s => s.ProductId);


            builder.HasOne(ss => ss.AttributeValue)
                .WithMany(s => s.ProductAttributeValues)
                .HasForeignKey(ss => ss.AttributeValueId);
        }
    }
}
