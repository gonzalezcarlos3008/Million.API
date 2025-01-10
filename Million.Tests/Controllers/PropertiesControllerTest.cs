using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Million.API.Controllers;
using Million.Application.DTOs;
using Million.Application.Services.Interfaces;
using Million.Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Million.Tests.Controllers
{
    [TestFixture]
    public class PropertiesControllerTests
    {
        private Mock<IPropertyService> _mockService;
        private PropertiesController _controller;

        [SetUp]
        public void SetUp()
        {
            _mockService = new Mock<IPropertyService>();
            _controller = new PropertiesController(_mockService.Object);
        }

        [Test]
        public async Task CreateProperty_ReturnsCreatedAtAction()
        {
            // Arrange
            var property = new PropertyCreateDto { Name = "Test Property" };
            _mockService.Setup(service => service.CreatePropertyAsync(property))
                        .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.CreateProperty(property);

            // Assert
            Assert.IsInstanceOf<CreatedAtActionResult>(result);
            var actionResult = result as CreatedAtActionResult;
            Assert.AreEqual(property, actionResult.Value);
        }

        [Test]
        public async Task ListProperties_ReturnsOkWithProperties()
        {
            // Arrange
            var properties = new List<Property> { new Property { Id = 1, Name = "Test Property" } };
            _mockService.Setup(service => service.ListPropertiesAsync(null))
                        .ReturnsAsync(properties);

            // Act
            var result = await _controller.ListProperties(null);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.AreEqual(properties, okResult.Value);
        }

        [Test]
        public async Task ChangePrice_ReturnsNoContent()
        {
            // Arrange
            var propertyId = 1;
            var newPrice = 500000M;

            _mockService.Setup(service => service.ChangePriceAsync(propertyId, newPrice))
                        .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.ChangePrice(propertyId, newPrice);

            // Assert
            Assert.IsInstanceOf<NoContentResult>(result);
            _mockService.Verify(service => service.ChangePriceAsync(propertyId, newPrice), Times.Once);
        }

        [Test]
        public async Task UpdateProperty_ReturnsNoContent()
        {
            // Arrange
            var property = new Property
            {
                Id = 1,
                Name = "Updated Property",
                Address = "Updated Address",
                Price = 750000M,
                CodeInternal = "UPD-123",
                Year = 2022,
                IdOwner = 2
            };

            _mockService.Setup(service => service.UpdatePropertyAsync(property))
                        .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.UpdateProperty(property);

            // Assert
            Assert.IsInstanceOf<NoContentResult>(result);
            _mockService.Verify(service => service.UpdatePropertyAsync(property), Times.Once);
        }

        [Test]
        public async Task UploadImage_ReturnsOkWithFilePath()
        {
            // Arrange
            var fileMock = new Mock<IFormFile>();
            var fileName = "test.jpg";
            var propertyId = 1;
            var applicationUrl = "http://localhost";
            fileMock.Setup(f => f.FileName).Returns(fileName);
            fileMock.Setup(f => f.Length).Returns(1024);
            var filePath = $"{applicationUrl}/uploads/{fileName}";

            _mockService.Setup(service => service.SavePropertyImageAsync(propertyId, fileMock.Object, applicationUrl))
                        .ReturnsAsync(filePath);

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    Request =
                    {
                        Scheme = "http",
                        Host = new HostString("localhost")
                    }
                }
            };

            // Act
            var result = await _controller.UploadImage(propertyId, fileMock.Object);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
        }
    }
}
