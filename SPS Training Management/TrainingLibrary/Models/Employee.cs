using System;
using System.Collections.Generic;

namespace TrainingLibrary.Models;

public partial class Employee
{
    public string EmpId { get; set; } = null!;

    public virtual ICollection<Trainee> Trainees { get; set; } = new List<Trainee>();
}
