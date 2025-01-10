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
    public class PropertyRepositoryTest
    {
        private RealEstateContext _context;
        private PropertyRepository _repository;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<RealEstateContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new RealEstateContext(options);
            _repository = new PropertyRepository(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [Test]
        public async Task GetByFilterAsync_ReturnsFilteredProperties()
        {
            // Arrange
            var properties = GetProperties();

            await _context.Properties.AddRangeAsync(properties);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetByFilterAsync("Test");

            // Assert
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("Test Property 1", result.First().Name);
        }

        [Test]
        public async Task GetByFilterAsync_ReturnsAllProperties_WhenFilterIsEmpty()
        {
            // Arrange
            var properties = GetProperties();

            await _context.Properties.AddRangeAsync(properties);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetByFilterAsync(string.Empty);

            // Assert
            Assert.AreEqual(2, result.Count());
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
