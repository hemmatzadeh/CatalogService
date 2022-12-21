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
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            //builder.HasKey(c => c.Id);
            //builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Property(c=>c.Name).HasMaxLength(150).IsRequired();
            builder.Property(c=>c.Price).IsRequired();
            builder.Property(c=>c.ProductCategoryId).IsRequired();

        }
    }
}
