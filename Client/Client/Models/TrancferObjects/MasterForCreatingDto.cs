using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models.TrancferObjects
{
    public record MasterForCreatingDto
    {
        public string? number { get; set; }
        public string? note { get; set; }
    }
}
