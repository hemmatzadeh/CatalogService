using AutoMapper;
using FH.CatalogService.Application.Mappings;
using FH.CatalogService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FH.CatalogService.Application.Dtos
{
    public class ProductCategoryAttributeDto 
    {
        public int AttributeId { get; set; }

        public string AttributeName { get; set; }

       
    }
}
