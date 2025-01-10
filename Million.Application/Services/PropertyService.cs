using Microsoft.AspNetCore.Http;
using Million.Application.DTOs;
using Million.Application.Services.Interfaces;
using Million.Domain.Entities;
using Million.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Million.Application.Services
{
    /// <summary>
    /// Implementation of property-related services.
    /// </summary>
    public class PropertyService : IPropertyService
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IPropertyImageRepository _propertyImageRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyService"/> class.
        /// </summary>
        /// <param name="propertyRepository">The property repository.</param>
        public PropertyService(IPropertyRepository propertyRepository, 
            IPropertyImageRepository propertyImageRepository)
        {
            _propertyRepository = propertyRepository;
            _propertyImageRepository = propertyImageRepository;
        }

        public async Task CreatePropertyAsync(PropertyCreateDto property)
        {
            var newProperty = new Property
            {
                Name = property.Name,
                Address = property.Address,
                Price = property.Price,
                CodeInternal = property.CodeInternal,
                Year = property.Year,
                IdOwner = property.IdOwner
            };
            await _propertyRepository.AddAsync(newProperty);
        }

        public async Task ChangePriceAsync(long propertyId, decimal newPrice)
        {
            var property = await _propertyRepository.GetByIdAsync(propertyId);
            if (property != null)
            {
                property.Price = newPrice;
                await _propertyRepository.UpdateAsync(property);
            }
        }

        public async Task UpdatePropertyAsync(Property property)
        {
            await _propertyRepository.UpdateAsync(property);
        }

        public async Task<IEnumerable<Property>> ListPropertiesAsync(string filter)
        {
            return await _propertyRepository.GetByFilterAsync(filter);
        }

        public async Task<string> SavePropertyImageAsync(long propertyId, IFormFile file, string applicationUrl)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("Invalid file.");
            }

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var fileUrl = $"{applicationUrl}/uploads/{fileName}";

            var propertyImage = new PropertyImage
            {
                IdProperty = propertyId,
                File = fileUrl,
                Enabled = true
            };

            await _propertyImageRepository.AddAsync(propertyImage);

            return fileUrl;
        }
    }
}
