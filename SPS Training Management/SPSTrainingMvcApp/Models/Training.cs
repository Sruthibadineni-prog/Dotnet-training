namespace SPSTrainingMvcApp.Models
{
    public class Training
    {
        public string TrainingId { get; set; } = null!;

        public string? TechId { get; set; }

        public string? TrainerId { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

    }
}
