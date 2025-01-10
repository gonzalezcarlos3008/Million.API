using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Million.Application.DTOs
{
    /// <summary>
    /// DTO for creating a property.
    /// </summary>
    public class PropertyCreateDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public decimal Price { get; set; }
        public string CodeInternal { get; set; }
        public int Year { get; set; }
        public long? IdOwner { get; set; }
    }
}
