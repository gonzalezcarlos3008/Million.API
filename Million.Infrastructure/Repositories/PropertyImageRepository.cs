using Million.Domain.Entities;
using Million.Infrastructure.Context;
using Million.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Million.Infrastructure.Repositories
{
    /// <summary>
    /// Implementation of the property image repository.
    /// </summary>
    public class PropertyImageRepository : Repository<PropertyImage>, IPropertyImageRepository
    {
        public PropertyImageRepository(RealEstateContext context) : base(context)
        {
        }
    }
}
