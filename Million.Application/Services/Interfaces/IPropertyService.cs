using Microsoft.AspNetCore.Http;
using Million.Application.DTOs;
using Million.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Million.Application.Services.Interfaces
{
    /// <summary>
    /// Interface for property-related services.
    /// </summary>
    public interface IPropertyService
    {
        /// <summary>
        /// Creates a new property.
        /// </summary>
        /// <param name="propertyDto">The property data transfer object.</param>
        Task CreatePropertyAsync(PropertyCreateDto property);
        
        /// <summary>
        /// Updates the price of an existing property.
        /// </summary>
        /// <param name="propertyId">The identifier of the property.</param>
        /// <param name="newPrice">The new price for the property.</param>
        Task ChangePriceAsync(long propertyId, decimal newPrice);
        
        /// <summary>
        /// Updates an existing property.
        /// </summary>
        /// <param name="property">The property to update.</param>
        Task UpdatePropertyAsync(Property property);
        
        /// <summary>
        /// Lists properties based on a filter.
        /// </summary>
        /// <param name="filter">The filter to apply.</param>
        /// <returns>A collection of properties matching the filter.</returns>
        Task<IEnumerable<Property>> ListPropertiesAsync(string filter);

        /// <summary>
        /// Saves an image for a property.
        /// </summary>
        /// <param name="propertyId">The identifier of the property.</param>
        /// <param name="file">The image file to save.</param>
        /// <param name="applicationUrl">The application base URL.</param>
        /// <returns>The URL of the saved image.</returns>
        Task<string> SavePropertyImageAsync(long propertyId, IFormFile file, string applicationUrl);
    }

}
