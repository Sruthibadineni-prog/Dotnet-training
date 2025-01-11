namespace SPSTrainingMvcApp.Models
{
    public class Employee
    {
        public int EmpId { get; set; }

        public string EmpName { get; set; } = null!;

        public string? EmpPhone { get; set; }

        public string? EmpEmail { get; set; }
    }
}
