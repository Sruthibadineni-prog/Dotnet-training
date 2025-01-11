//using Microsoft.AspNetCore.Mvc.Rendering;

//namespace SPSTrainingMvcApp.Models
//{
//    public class ForeignKeyHelper {
//        public static async Task<List<SelectListItem>> GetEmpIds() {
//            HttpClient client = new HttpClient() { BaseAddress = new Uri("http://employeewebapi-snrao.azurewebsites.net/api/Employee/") };
//            List<Employee> employees = await client.GetFromJsonAsync<List<Employee>>("");
//            List<SelectListItem> empIds = new List<SelectListItem>();
//            foreach (Employee employee in employees) {
//                empIds.Add(new SelectListItem { Text=employee.EmpId.ToString(), Value=employee.EmpId.ToString()});
//            }
//            return empIds;
//        }
//        public static async Task<List<SelectListItem>> GetTechIds() {
//            HttpClient client = new HttpClient() { BaseAddress = new Uri("http://technologywebapi-snrao.azurewebsites.net/api/Technology/") };
//            List<Technology> technologies = await client.GetFromJsonAsync<List<Technology>>("");
//            List<SelectListItem> techIds = new List<SelectListItem>();
//            foreach (Technology technology in technologies) {
//                techIds.Add(new SelectListItem { Text = technology.TechId, Value = technology.TechId });
//            }
//            return techIds;
//        }
//        public static async Task<List<SelectListItem>> GetTrainerIds() {
//            HttpClient client = new HttpClient() { BaseAddress = new Uri("http://trainerwebapi-snrao.azurewebsites.net/api/Trainer/") };
//            List<Trainer> trainers = await client.GetFromJsonAsync<List<Trainer>>("");
//            List<SelectListItem> trainerIds = new List<SelectListItem>();
//            foreach (Trainer trainer in trainers) {
//                trainerIds.Add(new SelectListItem { Text = trainer.TrainerId, Value = trainer.TrainerId });
//            }
//            return trainerIds;
//        }
//        public static async Task<List<SelectListItem>> GetTrainingIds() {
//            HttpClient client = new HttpClient() { BaseAddress = new Uri("http://trainingwebapi-snrao.azurewebsites.net/api/Training/") };
//            List<Training> trainings = await client.GetFromJsonAsync<List<Training>>("");
//            List<SelectListItem> trainingIds = new List<SelectListItem>();
//            foreach (Training training in trainings) {
//                trainingIds.Add(new SelectListItem { Text = training.TrainingId, Value = training.TrainingId });
//            }
//            return trainingIds;
//        }
//    }
//}
