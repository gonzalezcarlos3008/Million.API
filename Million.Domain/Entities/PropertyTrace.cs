using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Million.Domain.Entities
{
    /// <summary>
    /// Represents a trace of a property transaction.
    /// </summary>
    public class PropertyTrace
    {
        public long Id { get; set; }
        public long IdProperty { get; set; }
        public DateTime DateSale { get; set; }
        public string Name { get; set; }
        public double Value { get; set; }
        public double Tax { get; set; }
    }
}
