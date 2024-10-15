using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Detail
    {
        [Column("DetailId")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Detail name is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Name is 30 characters.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Detail price is a required field.")]
        public double? Price { get; set; }


        [ForeignKey(nameof(Master))]
        public Guid MasterId { get; set; }
        public Master? Master{ get; set; }
    }
}
