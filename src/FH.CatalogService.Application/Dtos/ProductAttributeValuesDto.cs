using FH.CatalogService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FH.CatalogService.Application.Dtos
{
    public class ProductAttributeValuesDto
    {
        public int AttributeId { get; set; }
        public string AttributeName { get; set; }
        public string AttributeValueName { get; set; }
    }
}
