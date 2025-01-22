using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EmployeeLibrary.Models;
using EmployeeLibrary.Repos;
using Microsoft.AspNetCore.Connections;
using System.Text;

namespace EmployeeWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        IEmployeeRepoAsync empRepo;
        public EmployeeController(IEmployeeRepoAsync repo)
        {
            empRepo = repo;
        }
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            List<Employee> employees = await empRepo.GetAllEmployees();
            return Ok(employees);
        }
        [HttpGet("{empId}")]
        public async Task<ActionResult> GetOne(int empId)
        {
            try
            {
                Employee emp = await empRepo.GetEmployeeByIdAsync(empId);
                return Ok(emp);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        
        [HttpPost("{token}")]
        public async Task<ActionResult> Insert(string token, Employee employee)
        {
            try
            {
                await empRepo.InsertEmployeeAsync(employee);
                HttpClient client = new HttpClient();
                //await client.PostAsJsonAsync("http://localhost:5285/api/Trainee/Employee", new { EmpId = employee.EmpId });
                await client.PostAsJsonAsync("http://trainingwebapi-snrao.azurewebsites.net/api/Trainee/Employee", new { EmpId = employee.EmpId });
                return Created($"api/Employee/{employee.EmpId}", employee);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{empId}")]
        public async Task<ActionResult> Update(int empId, Employee employee)
        {
            try
            {
                await empRepo.UpdateEmployeeAsync(empId, employee);
                return Ok(employee);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{empId}")]
        public async Task<ActionResult> Delete(int empId)
        {
            try
            {
                await empRepo.DeleteEmployeeAsync(empId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
