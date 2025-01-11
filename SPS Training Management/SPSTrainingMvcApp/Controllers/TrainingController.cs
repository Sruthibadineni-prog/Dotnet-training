using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SPSTrainingMvcApp.Models;
namespace SPSTrainingMvcApp.Controllers
{
    [Authorize]
    public class TrainingController : Controller {
       // static HttpClient client = new HttpClient() { BaseAddress = new Uri("https://trainingwebapi-snrao.azurewebsites.net/api/Training/") };
        static HttpClient client = new HttpClient() { BaseAddress = new Uri("http://localhost:5285/api/Training/") };
        static string token;
        public async Task<ActionResult> Index() {
            string userName = User.Identity.Name;
            string role = User.Claims.ToArray()[4].Value;
            string secretKey = "My name is Bond, James Bond the great";
            HttpClient client2 = new HttpClient();
            //token = await client2.GetStringAsync("https://authenticationwebapi-snrao.azurewebsites.net/api/Auth/" + userName + "/" + role + "/" + secretKey);
            token = await client2.GetStringAsync("http://localhost:5227/api/Auth/" + userName + "/" + role + "/" + secretKey);

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            List<Training> trainings = await client.GetFromJsonAsync<List<Training>>("");
            return View(trainings);
        }
        public async Task<ActionResult> Details(string trainingId) {
            Training training = await client.GetFromJsonAsync<Training>("" + trainingId);
            return View(training);
        }
        public ActionResult Create() {
            Training training = new Training();
            return View(training);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Training training) {
            try {
                await client.PostAsJsonAsync<Training>("", training);
                return RedirectToAction(nameof(Index));
            }
            catch {
                return View();
            }
        }
        [Route("Training/Edit/{trainingId}")]
        public async Task<ActionResult> Edit(string trainingId) {
            Training training = await client.GetFromJsonAsync<Training>("" + trainingId);
            return View(training);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Training/Edit/{trainingId}")]
        public async Task<ActionResult> Edit(string trainingId, Training training) {
            try {
                await client.PutAsJsonAsync<Training>("" + trainingId, training);
                return RedirectToAction(nameof(Index));
            }
            catch {
                return View();
            }
        }
        [Route("Training/Delete/{trainingId}")]
        public async Task<ActionResult> Delete(string trainingId) {
            Training training = await client.GetFromJsonAsync<Training>("" + trainingId);
            return View(training);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Training/Delete/{trainingId}")]
        public async Task<ActionResult> Delete(string trainingId, IFormCollection collection) {
            try {
                await client.DeleteAsync("" + trainingId);
                return RedirectToAction(nameof(Index));
            }
            catch {
                return View();
            }
        }
        public async Task<ActionResult> ByTechnology(string techId) {
            List<Training> trainings = await client.GetFromJsonAsync<List<Training>>("ByTechnology/" + techId);
            return View(trainings);
        }
        public async Task<ActionResult> ByTrainer(string trainerId) {
            List<Training> trainings = await client.GetFromJsonAsync<List<Training>>("ByTrainer/" + trainerId);
            return View(trainings);
        }
    }
}
