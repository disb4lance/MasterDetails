
namespace Shared.DataTransferObjects
{
    public record MasterForUpdateDto
    {
        public string? number { get; set; }
        public string? note { get; set; }
        // public double? SumPrice { get; set; }
        //public IEnumerable<DetailForCreatingDto>? Details { get; set; }
    }
}
