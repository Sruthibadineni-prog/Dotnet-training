using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrainingLibrary.Models;
using TrainingLibrary.Repos;

namespace TrainingWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TraineeController : ControllerBase
    {
        ITraineeRepoAsync trainRepo;
        public TraineeController(ITraineeRepoAsync repo)
        {
            trainRepo = repo;
        }
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            List<Trainee> trainList = await trainRepo.GetAllTraineesAsync();
            return Ok(trainList);
        }
        [HttpGet("{trainingId}/{traineeId}")]
        public async Task<ActionResult> GetOne(string trainingId, string traineeId)
        {
            try
            {
                Trainee trainee = await trainRepo.GetTraineeByIdAsync(trainingId, traineeId);
                return Ok(trainee);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult> Insert(Trainee trainee)
        {
            try
            {
                await trainRepo.InsertTraineeAsync(trainee);
                return Created($"api/Trainee/{trainee.TraineeId}", trainee);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("Employee")]
        public async Task<ActionResult> InsertEmployee(Employee employee)
        {
            await trainRepo.InsertEmployeeAsync(employee);
            return Created();
        }
        [HttpPut("{trainingId}/{traineeId}")]
        public async Task<ActionResult> Update(string trainingId, string traineeId, Trainee trainee)
        {
            try
            {
                await trainRepo.UpdateTraineeAsync(trainingId, traineeId, trainee);
                return Ok(trainee);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{trainingId}/{traineeId}")]
        public async Task<ActionResult> Delete(string trainingId, string traineeId)
        {
            try
            {
                await trainRepo.DeleteTraineeAsync(trainingId, traineeId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("ByTraining/{trainingId}")]
        public async Task<ActionResult> GetByTraining(string trainingId)
        {
            try
            {
                List<Trainee> trainees = await trainRepo.GetTraineesByTraining(trainingId);
                return Ok(trainees);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("ByTrainee/{traineeId}")]
        public async Task<ActionResult> GetByTrainee(string traineeId)
        {
            try
            {
                List<Trainee> trainees = await trainRepo.GetTrainingsByTrainee(traineeId);
                return Ok(trainees);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
