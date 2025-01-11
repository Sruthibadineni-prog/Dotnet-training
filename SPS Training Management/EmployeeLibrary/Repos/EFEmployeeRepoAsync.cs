using EmployeeLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeLibrary.Repos
{
    public class EFEmployeeRepoAsync : IEmployeeRepoAsync
    {
        SPSEmployeeDBContext ctx = new SPSEmployeeDBContext();

        public async Task DeleteEmployeeAsync(int empId)
        {
            Employee emp = await GetEmployeeByIdAsync(empId);
            ctx.Employees.Remove(emp);
            await ctx.SaveChangesAsync();
        }
        public async Task<List<Employee>> GetAllEmployees()
        {
            List<Employee> employees = await ctx.Employees.ToListAsync();
            return employees;
        }

        public async Task<Employee> GetEmployeeByIdAsync(int empId)
        {
            try
            {
                Employee emp = await (from e in ctx.Employees where e.EmpId == empId select e).FirstAsync();
                return emp;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task InsertEmployeeAsync(Employee employee)
        {
            await ctx.Employees.AddAsync(employee);
            await ctx.SaveChangesAsync();
        }

        public async Task UpdateEmployeeAsync(int empId, Employee employee)
        {
            Employee emp = await GetEmployeeByIdAsync(empId);
            emp.EmpPhone = employee.EmpPhone;
            emp.EmpName = employee.EmpName;
            emp.EmpEmail = employee.EmpEmail;
            await ctx.SaveChangesAsync();
        }
    }
}
