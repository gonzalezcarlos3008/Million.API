
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Million.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Million.Infrastructure.Context
{
    /// <summary>
    /// Represents the database context for the Real Estate application.
    /// </summary>
    public class RealEstateContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of <see cref="RealEstateContext"/> with the specified options.
        /// </summary>
        /// <param name="options">The options to be used by the DbContext.</param>
        public RealEstateContext(DbContextOptions<RealEstateContext> options) : base(options) { }

        /// <summary>
        /// Gets or sets the Properties table.
        /// </summary>
        public DbSet<Property> Properties { get; set; }

        /// <summary>
        /// Gets or sets the PropertyImages table.
        /// </summary>
        public DbSet<PropertyImage> PropertyImages { get; set; }

        /// <summary>
        /// Gets or sets the PropertyTraces table.
        /// </summary>
        public DbSet<PropertyTrace> PropertyTraces { get; set; }

        /// <summary>
        /// Gets or sets the Owners table.
        /// </summary>
        public DbSet<Owner> Owners { get; set; }

        /// <summary>
        /// Configures the entity mappings for the DbContext.
        /// </summary>
        /// <param name="modelBuilder">The model builder used to configure entity mappings.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Map the Property entity to the correct table name
            modelBuilder.Entity<Property>().ToTable("Property").Property(x => x.Id).HasColumnName("IdProperty");

            // Map the PropertyImage entity to the correct table name
            modelBuilder.Entity<PropertyImage>().ToTable("PropertyImage").Property(x => x.Id).HasColumnName("IdPropertyImage");

            // Map the PropertyTrace entity to the correct table name
            modelBuilder.Entity<PropertyTrace>().ToTable("PropertyTrace").Property(x => x.Id).HasColumnName("IdPropertyTrace");

            // Map the Owner entity to the correct table name
            modelBuilder.Entity<Owner>().ToTable("Owner").Property(x => x.Id).HasColumnName("IdOwner");
        }
    }
}
