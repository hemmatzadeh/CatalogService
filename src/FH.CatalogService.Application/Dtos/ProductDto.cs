using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FH.CatalogService.Application.Dtos
{
    public class ProductDto
    {
        public ProductDto()
        {
            ProductAttributeValues = new HashSet<ProductAttributeValuesDto>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public double Price { get; set; }

        public int CategotyId { get; set; }

        public string CategotyName { get; set; }

        public virtual ICollection<ProductAttributeValuesDto> ProductAttributeValues { get; set; }



    }
}
