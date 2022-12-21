using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FH.CatalogService.Domain.Entities
{
    public class ProductCategoryAttribute : BaseEntity
    {
        public int CategoryId { get; set; }

        public int AttributeId { get; set;}

        public ProductCategory ProductCategory { get; set; }

        public ProductAttribute ProductAttribute { get; set; }


    }
}
