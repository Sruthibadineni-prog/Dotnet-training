using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using TrainerLibrary.Models;
using TrainerLibrary.Repos;

namespace TrainerWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TrainerController : ControllerBase
    {
        ITrainerRepoAsync traRepo;
        public TrainerController(ITrainerRepoAsync repo)
        {
            traRepo = repo;
        }
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            List<Trainer> trainers = await traRepo.GetAllTrainersAsync();
            return Ok(trainers);
        }
        [HttpGet("{trainerId}")]
        public async Task<ActionResult> GetOne(string trainerId)
        {
            try
            {
                Trainer trainer = await traRepo.GetTrainerAsync(trainerId);
                return Ok(trainer);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("ByType/{type}")]
        public async Task<ActionResult> GetByType(string type)
        {
            List<Trainer> trainers = await traRepo.GetTrainersByTypeAsync(type);
            return Ok(trainers);
        }
        [HttpPost("{token}")]
        public async Task<ActionResult> Insert(string token, Trainer trainer)
        {
            try
            {
                await traRepo.InsertTrainerAsync(trainer);
                HttpClient client = new HttpClient();
                await client.PostAsJsonAsync("http://localhost:5285/api/Training/Trainer", new { TrainerId = trainer.TrainerId });
                //await client.PostAsJsonAsync("http://trainingwebapi-snrao.azurewebsites.net/api/Training/Trainer", new { TrainerId = trainer.TrainerId });
                return Created($"api/Trainer/{trainer.TrainerId}", trainer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{trainerId}")]
        public async Task<ActionResult> Update(string trainerId, Trainer trainer)
        {
            try
            {
                await traRepo.UpdateTrainerAsync(trainerId, trainer);
                return Ok(trainer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{trainerId}")]
        public async Task<ActionResult> Delete(string trainerId)
        {
            try
            {
                await traRepo.DeleteTrainerAsync(trainerId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
