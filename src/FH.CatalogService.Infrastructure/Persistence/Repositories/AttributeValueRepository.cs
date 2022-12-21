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
    public class AttributeValueRepository : GenericRepository<AttributeValue>, IAttributeValueRepository
    {
        public AttributeValueRepository(DatabaseContext Context) : base(Context)
        {
        }

        public async Task<AttributeValue> GetByAttributeValueNameAsync(string name)
        {
            return (await GetAsync(x => x.ValueName == name)).FirstOrDefault();
        }
    }
}
