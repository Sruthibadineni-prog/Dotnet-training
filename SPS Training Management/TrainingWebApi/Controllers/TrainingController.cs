using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrainingLibrary.Models;
using TrainingLibrary.Repos;

namespace TrainingWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TrainingController : ControllerBase
    {
        ITrainingRepoAsync trainRepo;
        public TrainingController(ITrainingRepoAsync repo)
        {
            trainRepo = repo;
        }
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            List<Training> training = await trainRepo.GetAllTrainingsAsync();
            return Ok(training);
        }
        [HttpGet("{trainingId}")]
        public async Task<ActionResult> GetOne(string trainingId)
        {
            try
            {
                Training training = await trainRepo.GetTrainingAsync(trainingId);
                return Ok(training);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult> Insert(Training training)
        {
            try
            {
                await trainRepo.InsertTrainingAsync(training);
                return Created($"api/Training/{training.TrainingId}", training);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("Technology")]
        public async Task<ActionResult> InsertTechnology(Technology technology)
        {
            await trainRepo.InsertTechnologyAsync(technology);
            return Created();
        }
        [HttpPost("Trainer")]
        public async Task<ActionResult> InsertTrainer(Trainer trainer)
        {
            await trainRepo.InsertTrainerAsync(trainer);
            return Created();
        }
        [HttpPut("{trainingId}")]
        public async Task<ActionResult> Update(string trainingId, Training training)
        {
            try
            {
                await trainRepo.UpdateTrainingAsync(trainingId, training);
                return Ok(training);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{trainingId}")]
        public async Task<ActionResult> Delete(string trainingId)
        {
            try
            {
                await trainRepo.DeleteTrainingAsync(trainingId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("ByTechnology/{techId}")]
        public async Task<ActionResult> GetByTechnology(string techId)
        {
            try
            {
                List<Training> trainings = await trainRepo.GetTrainingsByTechnologyAsync(techId);
                return Ok(trainings);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("ByTrainer/{trainerId}")]
        public async Task<ActionResult> GetByTrainer(string trainerId)
        {
            try
            {
                List<Training> trainings = await trainRepo.GetTrainingsByTrainerAsync(trainerId);
                return Ok(trainings);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
