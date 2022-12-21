using FH.CatalogService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FH.CatalogService.Application.Dtos
{
    public class AttributeValueDto
    {
        public int AttributeId { get; set; }

        public string ValueName { get; set; }

        public string? ValueDescription { get; set; }

        public ProductAttributeDto ProductAttribute { get; set; }
    }
}
