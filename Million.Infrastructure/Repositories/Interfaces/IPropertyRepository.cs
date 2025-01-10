using Million.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Million.Infrastructure.Repositories.Interfaces
{
    /// <summary>
    /// Interface for a property repository.
    /// </summary>
    public interface IPropertyRepository : IRepository<Property>
    {

        /// <summary>
        /// Retrieves properties matching a specific filter.
        /// </summary>
        /// <param name="filter">The filter to apply.</param>
        /// <returns>A collection of matching properties.</returns>
        Task<IEnumerable<Property>> GetByFilterAsync(string filter);
    }

}
