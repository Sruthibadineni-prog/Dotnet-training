using EmployeeLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeLibrary.Repos
{
    public interface IEmployeeRepoAsync
    {
        Task InsertEmployeeAsync(Employee employee);
        Task UpdateEmployeeAsync(int empId, Employee employee);
        Task DeleteEmployeeAsync(int empId);
        Task<Employee> GetEmployeeByIdAsync(int empId);
        Task<List<Employee>> GetAllEmployees();

    }
}
