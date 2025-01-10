using Microsoft.AspNetCore.Http;
using Million.Application.DTOs;
using Million.Application.Services;
using Million.Domain.Entities;
using Million.Infrastructure.Repositories.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Million.Tests.Services
{
    [TestFixture]
    public class PropertyServiceTests
    {
        private Mock<IPropertyRepository> _mockRepository;
        private Mock<IPropertyImageRepository> _mockRepositoryImage;
        private PropertyService _service;

        [SetUp]
        public void SetUp()
        {
            _mockRepository = new Mock<IPropertyRepository>();
            _mockRepositoryImage = new Mock<IPropertyImageRepository>();
            _service = new PropertyService(_mockRepository.Object, _mockRepositoryImage.Object);
        }

        [Test]
        public async Task CreatePropertyAsync_AddsProperty()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.AddAsync(It.IsAny<Property>()))
                           .Returns(Task.CompletedTask);

            // Act
            await _service.CreatePropertyAsync(new PropertyCreateDto { Name = "Test Property" });

            // Assert
            _mockRepository.Verify(repo => repo.AddAsync(It.IsAny<Property>()), Times.Once);
        }

        [Test]
        public async Task ChangePriceAsync_UpdatesPrice_WhenPropertyExists()
        {
            // Arrange
            var propertyId = 1;
            var newPrice = 500000M;
            var property = new Property { Id = propertyId, Price = 400000M };

            _mockRepository.Setup(repo => repo.GetByIdAsync(propertyId))
                           .ReturnsAsync(property);
            _mockRepository.Setup(repo => repo.UpdateAsync(property))
                           .Returns(Task.CompletedTask);

            // Act
            await _service.ChangePriceAsync(propertyId, newPrice);

            // Assert
            Assert.AreEqual(newPrice, property.Price);
            _mockRepository.Verify(repo => repo.UpdateAsync(property), Times.Once);
        }

        [Test]
        public async Task ChangePriceAsync_DoesNothing_WhenPropertyDoesNotExist()
        {
            // Arrange
            var propertyId = 1;
            var newPrice = 500000M;

            _mockRepository.Setup(repo => repo.GetByIdAsync(propertyId))
                           .ReturnsAsync((Property)null);

            // Act
            await _service.ChangePriceAsync(propertyId, newPrice);

            // Assert
            _mockRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Property>()), Times.Never);
        }

        [Test]
        public async Task UpdatePropertyAsync_UpdatesProperty()
        {
            // Arrange
            var property = new Property { Id = 1, Name = "Updated Property" };

            _mockRepository.Setup(repo => repo.UpdateAsync(property))
                           .Returns(Task.CompletedTask);

            // Act
            await _service.UpdatePropertyAsync(property);

            // Assert
            _mockRepository.Verify(repo => repo.UpdateAsync(property), Times.Once);
        }

        [Test]
        public async Task ListPropertiesAsync_ReturnsProperties()
        {
            // Arrange
            var properties = new List<Property>
        {
            new Property { Id = 1, Name = "Property 1" },
            new Property { Id = 2, Name = "Property 2" }
        };

            _mockRepository.Setup(repo => repo.GetByFilterAsync(null))
                           .ReturnsAsync(properties);

            // Act
            var result = await _service.ListPropertiesAsync(null);

            // Assert
            Assert.AreEqual(properties, result);
        }

        [Test]
        public async Task SavePropertyImageAsync_ReturnsFilePath()
        {
            // Arrange
            var propertyId = 1;
            var fileMock = new Mock<IFormFile>();
            var fileName = "test.jpg";
            var applicationUrl = "http://localhost";
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
            var filePath = Path.Combine(uploadsFolder, fileName);
            fileMock.Setup(f => f.FileName).Returns(fileName);
            fileMock.Setup(f => f.Length).Returns(1024);

            _mockRepositoryImage.Setup(repo => repo.AddAsync(It.IsAny<PropertyImage>()))
                           .Returns(Task.CompletedTask);

            // Act
            var result = await _service.SavePropertyImageAsync(propertyId, fileMock.Object, applicationUrl);

            // Assert
            Assert.IsTrue(result.Contains($"{applicationUrl}/uploads/"));
            _mockRepositoryImage.Verify(repo => repo.AddAsync(It.IsAny<PropertyImage>()), Times.Once);
        }

        [Test]
        public void SavePropertyImageAsync_ThrowsException_WhenFileIsInvalid()
        {
            // Arrange
            IFormFile file = null;
            var propertyId = 1;
            var applicationUrl = "http://localhost";

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(() => _service.SavePropertyImageAsync(propertyId, file, applicationUrl));
        }

    }
}
