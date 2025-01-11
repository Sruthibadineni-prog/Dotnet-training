namespace SPSTrainingMvcApp.Models
{
    public class Trainer
    {
        public string TrainerId { get; set; } = null!;

        public string TrainerName { get; set; } = null!;

        public string? TrainerType { get; set; }

        public string? TrainerPhone { get; set; }

        public string? TrainerEmail { get; set; }
    }
}
