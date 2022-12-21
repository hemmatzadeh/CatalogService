
using AutoMapper;
using FH.CatalogService.Application.Mappings;
using FH.CatalogService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FH.CatalogService.Application.Dtos
{
    public class CategoryDto 
    {
        public CategoryDto()
        {
            CategoryAttributes = new List<ProductCategoryAttributeDto>();
        }
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public virtual IList<ProductCategoryAttributeDto> CategoryAttributes { get; set; }

        
    }
}
