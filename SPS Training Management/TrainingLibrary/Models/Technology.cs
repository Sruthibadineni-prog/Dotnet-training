using System;
using System.Collections.Generic;

namespace TrainingLibrary.Models;

public partial class Technology
{
    public string TechId { get; set; } = null!;

    public virtual ICollection<Training> Training { get; set; } = new List<Training>();
}
