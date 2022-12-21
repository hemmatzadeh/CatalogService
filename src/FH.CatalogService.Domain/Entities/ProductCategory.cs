using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FH.CatalogService.Domain.Entities
{
    public class ProductCategory : BaseEntity
    {
        public ProductCategory()
        {
            ProductCategoryAttributes = new HashSet<ProductCategoryAttribute>();
        }
        public string Name { get; set; }

        public string? Description { get; set; }

        public bool IsDeleted { get; set; }

        public ICollection<Product> Products { get; set; }

        public ICollection<ProductCategoryAttribute> ProductCategoryAttributes { get; set; }

    }
}
