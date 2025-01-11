using System;
using System.Collections.Generic;

namespace TechnologyLibrary.Models;

public partial class Technology
{
    public string TechId { get; set; } = null!;

    public string TechTitle { get; set; } = null!;

    public string? TechLevel { get; set; }

    public int? Duration { get; set; }
}
