using FH.CatalogService.Application.Abstraction.Repositories;
using FH.CatalogService.Application.Abstraction.Repositories.Base;
using FH.CatalogService.Application.Specifications.Categories;
using FH.CatalogService.Domain.Entities;
using FH.CatalogService.Infrastructure.Persistence.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FH.CatalogService.Infrastructure.Persistence.Repositories
{
    public class ProductAttributeRepository : GenericRepository<ProductAttribute>, IProductAttributeRepository
    {
        public ProductAttributeRepository(DatabaseContext Context) : base(Context)
        {
        }

        public async Task<ProductAttribute> GetByAttributeNameAsync(string name)
        {
            return (await GetAsync(x => x.Name == name)).FirstOrDefault();
        }


    }
}
