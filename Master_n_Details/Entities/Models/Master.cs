
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace Entities.Models
{
    public class Master
    {
        [Column("MasterId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Master number is a required field.")]
        [MaxLength(10, ErrorMessage = "Maximum length for the number is 10 characters.")]
        public string? Number { get; set; }
        public DateTime? Date { get; set; }

        public double? SumPrices { get; set; }

        public string? note { get; set; }

        public ICollection<Detail>? Details { get; set; }
    }
}
