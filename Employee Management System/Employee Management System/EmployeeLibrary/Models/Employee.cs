using System;
using System.Collections.Generic;

namespace EmployeeLibrary.Models;

public partial class Employee
{
    public int EmpId { get; set; }

    public string EmpName { get; set; } = null!;

    public string? EmpPhone { get; set; }

    public string? EmpEmail { get; set; }
}
