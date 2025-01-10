using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Million.Domain.Entities
{
    /// <summary>
    /// Represents an image associated with a property.
    /// </summary>
    public class PropertyImage
    {
        public long Id { get; set; }
        public long IdProperty { get; set; }
        public string File { get; set; }
        public bool Enabled { get; set; }
    }
}
