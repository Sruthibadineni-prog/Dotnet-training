using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SPSTrainingMvcApp.Models;
namespace SPSTrainingMvcApp.Controllers
{
    [Authorize]
    public class EmployeeController : Controller {
        //static HttpClient client = new HttpClient() { BaseAddress = new Uri("https://employeewebapi-snrao.azurewebsites.net/api/Employee/") };
        static HttpClient client = new HttpClient() { BaseAddress = new Uri("http://localhost:5058/api/Employee/") };
        static string token;
        public async Task<ActionResult> Index() {
            string userName = User.Identity.Name;
            string role = User.Claims.ToArray()[4].Value;
            string secretKey = "My name is Bond, James Bond the great";
            HttpClient client2 = new HttpClient();
           // token = await client2.GetStringAsync("https://authenticationwebapi-snrao.azurewebsites.net/api/Auth/" + userName + "/" + role + "/" + secretKey);
            token = await client2.GetStringAsync("http://localhost:5227/api/Auth/" + userName + "/" + role + "/" + secretKey);

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            List<Employee> employees = await client.GetFromJsonAsync<List<Employee>>("");
            return View(employees);
        }
        public async Task<ActionResult> Details(int eid) {
            Employee employee = await client.GetFromJsonAsync<Employee>("" + eid);
            return View(employee);
        }
        public ActionResult Create() {
            Employee employee = new Employee();
            return View(employee);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Employee employee) {
            try {
                await client.PostAsJsonAsync<Employee>("" + token, employee);
                return RedirectToAction(nameof(Index));
            }
            catch {
                return View();
            }
        }
        [Route("Employee/Edit/{eid}")]
        public async Task<ActionResult> Edit(int eid) {
            Employee employee = await client.GetFromJsonAsync<Employee>("" + eid);
            return View(employee);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Employee/Edit/{eid}")]
        public async Task<ActionResult> Edit(int eid, Employee employee) {
            try {
                await client.PutAsJsonAsync<Employee>("" + eid, employee);
                return RedirectToAction(nameof(Index));
            }
            catch {
                return View();
            }
        }
        [Route("Employee/Delete/{eid}")]
        public async Task<ActionResult> Delete(int eid) {
            Employee employee = await client.GetFromJsonAsync<Employee>("" + eid);
            return View(employee);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Employee/Delete/{eid}")]
        public async Task<ActionResult> Delete(int eid, IFormCollection collection) {
            try {
                await client.DeleteAsync("" + eid);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
