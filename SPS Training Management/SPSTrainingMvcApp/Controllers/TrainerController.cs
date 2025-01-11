using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SPSTrainingMvcApp.Models;
using System.Security.Cryptography.Xml;
namespace SPSTrainingMvcApp.Controllers
{
    [Authorize]
    public class TrainerController : Controller {
       // static HttpClient client = new HttpClient() { BaseAddress = new Uri("https://trainerwebapi-snrao.azurewebsites.net/api/Trainer/") };
        static HttpClient client = new HttpClient() { BaseAddress = new Uri("http://localhost:5158/api/Trainer/") };

        static string token;
        public async Task<ActionResult> Index() {
            string userName = User.Identity.Name;
            string role = User.Claims.ToArray()[4].Value;
            string secretKey = "My name is Bond, James Bond the great";
            HttpClient client2 = new HttpClient();
            //token = await client2.GetStringAsync("https://authenticationwebapi-snrao.azurewebsites.net/api/Auth/" + userName + "/" + role + "/" + secretKey);
            token = await client2.GetStringAsync("http://localhost:5227/api/Auth/" + userName + "/" + role + "/" + secretKey);

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            List<Trainer> trainers = await client.GetFromJsonAsync<List<Trainer>>("");
            return View(trainers);
        }
        public async Task<ActionResult> Details(string trainerId) {
            Trainer trainer = await client.GetFromJsonAsync<Trainer>("" + trainerId);
            return View(trainer);
        }
        public ActionResult Create() {
            Trainer trainer = new Trainer();
            return View(trainer);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Trainer trainer) {
            try {
                await client.PostAsJsonAsync<Trainer>("" + token, trainer);
                return RedirectToAction(nameof(Index));
            }
            catch {
                return View();
            }
        }
        [Route("Trainer/Edit/{trainerId}")]
        public async Task<ActionResult> Edit(string trainerId) {
            Trainer trainer = await client.GetFromJsonAsync<Trainer>("" + trainerId);
            return View(trainer);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Trainer/Edit/{trainerId}")]
        public async Task<ActionResult> Edit(string trainerId, Trainer trainer) {
            try {
                await client.PutAsJsonAsync("" + trainerId, trainer);
                return RedirectToAction(nameof(Index));
            }
            catch {
                return View();
            }
        }
        [Route("Trainer/Delete/{trainerId}")]
        public async Task<ActionResult> Delete(string trainerId) { 
            Trainer trainer = await client.GetFromJsonAsync<Trainer>("" + trainerId);
            return View(trainer);
        }
        [Route("Trainer/Delete/{trainerId}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(string trainerId, IFormCollection collection) {
            try {
                await client.DeleteAsync("" + trainerId);
                return RedirectToAction(nameof(Index));
            }
            catch {
                return View();
            }
        }
        public async Task<ActionResult> ByType(string type) {
            List<Trainer> trainers = await client.GetFromJsonAsync<List<Trainer>>("ByType/" + type);
            return View(trainers);
        }
    }
}
