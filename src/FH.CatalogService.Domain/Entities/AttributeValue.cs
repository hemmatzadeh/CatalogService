using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FH.CatalogService.Domain.Entities
{
    public class AttributeValue : BaseEntity
    {
        public int AttributeId { get; set; }

        public string ValueName { get; set; }

        public string? ValueDescription { get; set; }

        public ProductAttribute ProductAttribute { get; set; }

        public ICollection<ProductAttributeValues> ProductAttributeValues { get; set; }


    }
}