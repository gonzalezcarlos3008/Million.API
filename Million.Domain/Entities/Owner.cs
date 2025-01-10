using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Million.Domain.Entities
{
    /// <summary>
    /// Represents an owner of a property.
    /// </summary>
    public class Owner
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Photo { get; set; }
        public DateTime Birthday { get; set; }
    }
}
