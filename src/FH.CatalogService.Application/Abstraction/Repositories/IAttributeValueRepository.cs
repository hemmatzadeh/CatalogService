using FH.CatalogService.Application.Abstraction.Repositories.Base;
using FH.CatalogService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FH.CatalogService.Application.Abstraction.Repositories
{
    public interface IAttributeValueRepository : IGenericRepository<AttributeValue>
    {
        Task<AttributeValue> GetByAttributeValueNameAsync(string name);
    }
}
