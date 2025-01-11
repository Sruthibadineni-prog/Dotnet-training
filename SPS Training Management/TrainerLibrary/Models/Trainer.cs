using System;
using System.Collections.Generic;

namespace TrainerLibrary.Models;

public partial class Trainer
{
    public string TrainerId { get; set; } = null!;

    public string TrainerName { get; set; } = null!;

    public string TrainerType { get; set; } = null!;

    public string? TrainerPhone { get; set; }

    public string? TrainerEmail { get; set; }
}
