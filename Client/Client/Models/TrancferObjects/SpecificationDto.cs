using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models.TrancferObjects
{
    public record SpecificationDto
    {
        public Guid id { get; set; }
        public string name { get; set; }

        public double price { get; set; }
    }
}
