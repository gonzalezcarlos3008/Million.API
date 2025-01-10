using Microsoft.EntityFrameworkCore;
using Million.Domain.Entities;
using Million.Infrastructure.Context;
using Million.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Million.Tests.Repositories
{
    [TestFixture]
    public class RepositoryTest
    {
        private RealEstateContext _context;
        private Repository<Property> _repository;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<RealEstateContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new RealEstateContext(options);
            _repository = new Repository<Property>(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [Test]
        public async Task AddAsync_AddsEntityToDatabase()
        {
            // Arrange
            var property = GetProperties().FirstOrDefault();
            // Act
            await _repository.AddAsync(property);
            var result = await _repository.GetByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(property.Name, result.Name);
        }

        [Test]
        public async Task GetAllAsync_ReturnsAllEntities()
        {
            // Arrange
            var properties = GetProperties();

            await _context.Properties.AddRangeAsync(properties);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetAllAsync();

            // Assert
            Assert.AreEqual(2, result.Count());
        }

        [Test]
        public async Task GetByIdAsync_ReturnsEntityById()
        {
            // Arrange
            var property = GetProperties().FirstOrDefault();
            await _context.Properties.AddAsync(property);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(property.Name, result.Name);
        }

        [Test]
        public async Task UpdateAsync_UpdatesEntityInDatabase()
        {
            // Arrange
            var property = GetProperties().FirstOrDefault();
            await _context.Properties.AddAsync(property);
            await _context.SaveChangesAsync();

            property.Name = "Updated Name";

            // Act
            await _repository.UpdateAsync(property);
            var result = await _repository.GetByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual("Updated Name", result.Name);
        }

        [Test]
        public async Task DeleteAsync_RemovesEntityFromDatabase()
        {
            // Arrange
            var property = GetProperties().FirstOrDefault();
            await _context.Properties.AddAsync(property);
            await _context.SaveChangesAsync();

            // Act
            await _repository.DeleteAsync(1);
            var result = await _repository.GetByIdAsync(1);

            // Assert
            Assert.Null(result);
        }

        private List<Property> GetProperties()
        {
            return new List<Property>
            {
                new Property { Id = 1, Name = "Test Property 1", Address = "Address 1", CodeInternal ="ABC" },
                new Property { Id = 2, Name = "Sample Property 2", Address = "Address 1", CodeInternal ="ABD" }
            };
        }
    }
}
