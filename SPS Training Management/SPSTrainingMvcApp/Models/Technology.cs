namespace SPSTrainingMvcApp.Models
{
    public class Technology
    {
        public string TechId { get; set; } = null!;

        public string TechTitle { get; set; } = null!;

        public string? TechLevel { get; set; }

        public int? Duration { get; set; }
    }
}
