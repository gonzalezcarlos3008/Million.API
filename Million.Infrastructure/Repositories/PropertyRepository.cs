using Microsoft.EntityFrameworkCore;
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
    /// Implementation of the property repository.
    /// </summary>
    public class PropertyRepository : Repository<Property>, IPropertyRepository
    {
        private RealEstateContext _context;
        public PropertyRepository(RealEstateContext context) : base(context) { 
            _context = context;
        }

        public async Task<IEnumerable<Property>> GetByFilterAsync(string filter)
        {
            return await _context.Properties
                .Where(p => string.IsNullOrEmpty(filter) || p.Name.Contains(filter) || p.Year.ToString().Contains(filter)
                || p.Price.ToString().Contains(filter) || p.Address.ToString().Contains(filter))
                .ToListAsync();
        }
    }
}
