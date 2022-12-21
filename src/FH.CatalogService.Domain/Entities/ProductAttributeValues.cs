using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FH.CatalogService.Domain.Entities
{
    public class ProductAttributeValues
    {
        public int AttributeValueId { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }

        public AttributeValue AttributeValue { get; set; }
    }
}
