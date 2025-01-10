using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Million.Domain.Entities
{
    /// <summary>
    /// Represents a property in the real estate system.
    /// </summary>
    public class Property
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public decimal Price { get; set; }
        public string CodeInternal { get; set; }
        public int Year { get; set; }
        public long? IdOwner { get; set; }
    }
}
