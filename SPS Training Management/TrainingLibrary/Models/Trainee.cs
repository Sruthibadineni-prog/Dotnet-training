using System;
using System.Collections.Generic;

namespace TrainingLibrary.Models;

public partial class Trainee
{
    public string TrainingId { get; set; } = null!;

    public string TraineeId { get; set; } = null!;

    public string TraineeStatus { get; set; } = null!;

    public string? Remarks { get; set; }

    public virtual Employee TraineeNavigation { get; set; } = null!;

    public virtual Training Training { get; set; } = null!;
}
