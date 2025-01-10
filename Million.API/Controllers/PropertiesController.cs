using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Million.Application.DTOs;
using Million.Application.Services.Interfaces;
using Million.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;

namespace Million.API.Controllers
{
    /// <summary>
    /// Controller for managing properties.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class PropertiesController : ControllerBase
    {
        private readonly IPropertyService _propertyService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertiesController"/> class.
        /// </summary>
        /// <param name="propertyService">The property service.</param>
        public PropertiesController(IPropertyService propertyService)
        {
            _propertyService = propertyService;
        }

        /// <summary>
        /// Retrieves a list of properties with an optional filter.
        /// </summary>
        /// <param name="filter">The filter to apply to the properties list.</param>
        /// <returns>A list of properties matching the filter.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "List properties", Description = "Lists properties with optional filters")]
        public async Task<IActionResult> ListProperties([FromQuery] string? filter)
        {
            var properties = await _propertyService.ListPropertiesAsync(filter);
            return Ok(properties);
        }

        /// <summary>
        /// Creates a new property.
        /// </summary>
        /// <param name="propertyDto">The data transfer object for creating a property.</param>
        /// <returns>A response indicating the result of the operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(Summary = "Create a new property", Description = "Creates a new property with its owner and details")]
        public async Task<IActionResult> CreateProperty([FromBody] PropertyCreateDto property)
        {
            await _propertyService.CreatePropertyAsync(property);
            return CreatedAtAction(nameof(CreateProperty), property);
        }

        /// <summary>
        /// Updates the price of a property.
        /// </summary>
        /// <param name="propertyId">The identifier of the property.</param>
        /// <param name="newPrice">The new price to set.</param>
        /// <returns>A response indicating the result of the operation.</returns>
        [HttpPut("{propertyId}/price")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(Summary = "Update property price", Description = "Updates the price of a specific property")]
        public async Task<IActionResult> ChangePrice(long propertyId, [FromBody] decimal newPrice)
        {
            await _propertyService.ChangePriceAsync(propertyId, newPrice);
            return NoContent();
        }

        /// <summary>
        /// Updates a property.
        /// </summary>
        /// <param name="property">The property details to update.</param>
        /// <returns>A response indicating the result of the operation.</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(Summary = "Update property details", Description = "Updates the details of a specific property")]
        public async Task<IActionResult> UpdateProperty([FromBody] Property property)
        {
            await _propertyService.UpdatePropertyAsync(property);
            return NoContent();
        }

        /// <summary>
        /// Uploads an image for a property.
        /// </summary>
        /// <param name="propertyId">The identifier of the property.</param>
        /// <param name="file">The image file to upload.</param>
        /// <returns>A response indicating the result of the operation, including the file path.</returns>
        [HttpPost("{propertyId}/upload-image")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(Summary = "Upload property image", Description = "Uploads an image for a specific property")]
        public async Task<IActionResult> UploadImage(long propertyId, IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Invalid file.");
            }

            var applicationUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";
            var filePath = await _propertyService.SavePropertyImageAsync(propertyId, file, applicationUrl);
            return Ok(new { FilePath = filePath });
        }
    }
}
