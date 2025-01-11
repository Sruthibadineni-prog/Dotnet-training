using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using TechnologyLibrary.Models;
using TechnologyLibrary.Repos;

namespace TechnologyWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TechnologyController : ControllerBase
    {
        ITechnologyRepoAsync techRepo;
        public TechnologyController(ITechnologyRepoAsync repo)
        {
            techRepo = repo;
        }
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            List<Technology> technologies = await techRepo.GetAllTechnologies();
            return Ok(technologies);
        }
        [HttpGet("{techId}")]
        public async Task<ActionResult> GetOne(string techId)
        {
            try
            {
                Technology tech = await techRepo.GetTechnologyByIdAsync(techId);
                return Ok(tech);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("ByLevel/{level}")]
        public async Task<ActionResult> GetByLevel(string level)
        {
            List<Technology> technologies = await techRepo.GetTechnologiesByLevelAsync(level);
            return Ok(technologies);
        }
        [HttpPost("{token}")]
        public async Task<ActionResult> Insert(string token, Technology technology)
        {
            try
            {
                await techRepo.InsertTechnologyAsync(technology);
                HttpClient client = new HttpClient();
                await client.PostAsJsonAsync("http://localhost:5285/api/Training/Technology", new { TechId = technology.TechId });
                //await client.PostAsJsonAsync("http://trainingwebapi-snrao.azurewebsites.net/api/Training/Technology", new { TechId = technology.TechId });
                return Created($"api/Technology/{technology.TechId}", technology);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{techId}")]
        public async Task<ActionResult> Update(string techId, Technology technology)
        {
            try
            {
                await techRepo.UpdateTechnologyAsync(techId, technology);
                return Ok(technology);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{techId}")]
        public async Task<ActionResult> Delete(string techId)
        {
            try
            {
                await techRepo.DeleteTechnologyAsync(techId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
