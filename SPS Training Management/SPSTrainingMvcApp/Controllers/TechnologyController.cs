using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SPSTrainingMvcApp.Models;
namespace SPSTrainingMvcApp.Controllers
{
    [Authorize]
    public class TechnologyController : Controller {
        //static HttpClient client = new HttpClient() { BaseAddress = new Uri("https://technologywebapi-snrao.azurewebsites.net/api/Technology/") };
        static HttpClient client = new HttpClient() { BaseAddress = new Uri("http://localhost:5200/api/Technology/") };
        static string token;
        public async Task<ActionResult> Index() {
            string userName = User.Identity.Name;
            string role = User.Claims.ToArray()[4].Value;
            string secretKey = "My name is Bond, James Bond the great";
            HttpClient client2 = new HttpClient();
            //token = await client2.GetStringAsync("https://authenticationwebapi-snrao.azurewebsites.net/api/Auth/" + userName + "/" + role + "/" + secretKey);
            token = await client2.GetStringAsync("http://localhost:5227/api/Auth/" + userName + "/" + role + "/" + secretKey);

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            List<Technology> technologies = await client.GetFromJsonAsync<List<Technology>>("");
            return View(technologies);
        }
        public async Task<ActionResult> Details(string techId) {
            Technology technology = await client.GetFromJsonAsync<Technology>("" + techId);
            return View(technology);
        }
        public ActionResult Create() { 
            Technology technology = new Technology();
            return View(technology);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Technology technology) {
            try {
                await client.PostAsJsonAsync<Technology>("" + token, technology);
                return RedirectToAction(nameof(Index));
            }
            catch {
                return View();
            }
        }
        [Route("Technology/Edit/{techId}")]
        public async Task<ActionResult> Edit(string techId) {
            Technology technology = await client.GetFromJsonAsync<Technology>("" + techId);
            return View(technology);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Technology/Edit/{techId}")]
        public async Task<ActionResult> Edit(string techId, Technology technology) {
            try {
                await client.PutAsJsonAsync("" + techId, technology);
                return RedirectToAction(nameof(Index));
            }
            catch {
                return View();
            }
        }
        [Route("Technology/Delete/{techId}")]
        public async Task<ActionResult> Delete(string techId) {
            Technology technology = await client.GetFromJsonAsync<Technology>("" + techId);
            return View(technology);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Technology/Delete/{techId}")]
        public async Task<ActionResult> Delete(string techId, IFormCollection collection) {
            try {
                await client.DeleteAsync("" + techId);
                return RedirectToAction(nameof(Index));
            }
            catch {
                return View();
            }
        }
        public async Task<ActionResult> ByLevel(string level) {
            List<Technology> technologies  = await client.GetFromJsonAsync<List<Technology>>("ByLevel/" + level);
            return View(technologies);
        }
    }
}
