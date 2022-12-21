using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FH.CatalogService.Domain.Entities
{
    public class Product:BaseEntity
    {
        public string Name { get; set; }

        public int ProductCategoryId { get; set;}

        public double Price { get; set; }

        public bool IsDeleted { get; set; } 

        public ProductCategory ProductCategory { get; set;}

        public ICollection<ProductAttributeValues>  ProductAttributeValues { get; set; }
    }
}
