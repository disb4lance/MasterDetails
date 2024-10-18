using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models.TrancferObjects
{
    public record MasterDto
    {
        public Guid Id { get; set; }
        public string? Number { get; set; }

        public DateTime? Date { get; set; }
        public double? SumPrices { get; set; }
        public string? note { get; set; }
    }
}
