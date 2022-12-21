using FH.CatalogService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FH.CatalogService.Infrastructure
{
    public class DatabaseContextInitialiser
    {
        private readonly DatabaseContext context;
        private readonly ILogger<DatabaseContextInitialiser> logger;

        public DatabaseContextInitialiser(DatabaseContext databaseContext, ILogger<DatabaseContextInitialiser> logger)
        {
            this.context = databaseContext;
            this.logger = logger;
        }


        public async Task InitialiseAsync()
        {
            try
            {
                if (context.Database.IsSqlServer())
                {
                    await context.Database.MigrateAsync();
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while initialising the database.");
                throw;
            }
        }

        public async Task SeedAsync()
        {
            try
            {
                await TrySeedAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while seeding the database.");
                throw;
            }
        }

        private async Task TrySeedAsync()
        {
            // Categories 
            var catgoryTShirt = new ProductCategory { Name = "TShirt" };
            var catgoryShoes = new ProductCategory { Name = "Shoes" };

            // Products
            var proTshirt1 = new Product { Name = "SampleTshirt-1", ProductCategory = catgoryTShirt };
            var proTshirt2 = new Product { Name = "SampleTshirt-2", ProductCategory = catgoryTShirt };
            var proShoes1 = new Product { Name = "SampleShoes-1", ProductCategory = catgoryShoes };

            // Attributes
            var atrColor = new ProductAttribute { Name = "Color" };
            var atrSize = new ProductAttribute { Name = "Size" };
            var atrGender = new ProductAttribute { Name = "Gender" };
            var atrBrand = new ProductAttribute { Name = "Brand" };


            // Attributes Values
            var redAtrValue = new AttributeValue { ProductAttribute = atrColor, ValueName = "Red" };
            var greenAtrValue = new AttributeValue { ProductAttribute = atrColor, ValueName = "Green" };
            var blueAtrValue = new AttributeValue { ProductAttribute = atrColor, ValueName = "Blue" };
            var smallAtrValue = new AttributeValue { ProductAttribute = atrSize, ValueName = "Small" };
            var mediumAtrValue = new AttributeValue { ProductAttribute = atrSize, ValueName = "Medium" };
            var largAtrValue = new AttributeValue { ProductAttribute = atrSize, ValueName = "Larg" };
            var menAtrValue = new AttributeValue { ProductAttribute = atrGender, ValueName = "Men" };
            var womenAtrValue = new AttributeValue { ProductAttribute = atrGender, ValueName = "Women" };
            var calvAtrValue = new AttributeValue { ProductAttribute = atrBrand, ValueName = "Calvin Klein" };
            var nikeAtrValue = new AttributeValue { ProductAttribute = atrBrand, ValueName = "Nike" };

            // Product Attributes
            var proRedAttr = new ProductAttributeValues { Product = proTshirt1, AttributeValue = redAtrValue };
            var proSmallAttr = new ProductAttributeValues { Product = proTshirt1, AttributeValue = smallAtrValue };


            if (!context.ProductCategories.Any())
            {
                context.ProductCategories.Add(catgoryTShirt);
                context.ProductCategories.Add(catgoryShoes);

                await context.SaveChangesAsync();
            }

            if (!context.Products.Any())
            {
                context.Products.Add(proTshirt1);
                context.Products.Add(proTshirt2);
                context.Products.Add(proShoes1);

                await context.SaveChangesAsync();
            }

            if (!context.Attributes.Any())
            {
                context.Attributes.Add(atrSize);
                context.Attributes.Add(atrBrand);
                context.Attributes.Add(atrGender);
                context.Attributes.Add(atrColor);

                await context.SaveChangesAsync();
            }

            if (!context.CategoryAttributes.Any())
            {

                context.CategoryAttributes.Add(new ProductCategoryAttribute { ProductAttribute = atrColor, ProductCategory = catgoryTShirt });
                context.CategoryAttributes.Add(new ProductCategoryAttribute { ProductAttribute = atrSize, ProductCategory = catgoryTShirt });
                context.CategoryAttributes.Add(new ProductCategoryAttribute { ProductAttribute = atrGender, ProductCategory = catgoryTShirt });
                context.CategoryAttributes.Add(new ProductCategoryAttribute { ProductAttribute = atrBrand, ProductCategory = catgoryTShirt });

                await context.SaveChangesAsync();
            }

            if (!context.AttributeValues.Any())
            {


                context.AttributeValues.Add(redAtrValue);
                context.AttributeValues.Add(greenAtrValue);
                context.AttributeValues.Add(blueAtrValue);
                context.AttributeValues.Add(smallAtrValue);
                context.AttributeValues.Add(mediumAtrValue);
                context.AttributeValues.Add(largAtrValue);
                context.AttributeValues.Add(menAtrValue);
                context.AttributeValues.Add(womenAtrValue);
                context.AttributeValues.Add(calvAtrValue);
                context.AttributeValues.Add(nikeAtrValue);

                await context.SaveChangesAsync();
            }

            if (!context.ProductAttributeValues.Any())
            {
                context.ProductAttributeValues.Add(proRedAttr);
                context.ProductAttributeValues.Add(proSmallAttr);

                await context.SaveChangesAsync();
            }
        }
    }
}
