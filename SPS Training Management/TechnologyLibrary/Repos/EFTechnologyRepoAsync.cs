using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnologyLibrary.Models;

namespace TechnologyLibrary.Repos
{
    public class EFTechnologyRepoAsync : ITechnologyRepoAsync
    {
        SPSTechnologyDBContext ctx = new SPSTechnologyDBContext();
        public async Task DeleteTechnologyAsync(string TechId)
        {
            Technology tech = await GetTechnologyByIdAsync(TechId);
            ctx.Technologies.Remove(tech);
            await ctx.SaveChangesAsync();
        }
        public async Task<List<Technology>> GetAllTechnologies()
        {
            List<Technology> techs = await ctx.Technologies.ToListAsync();
            return techs;
        }
        public async Task<List<Technology>> GetTechnologiesByLevelAsync(string level)
        {
            List<Technology> technologies = await (from tech in ctx.Technologies where tech.TechLevel == level select tech).ToListAsync();
            return technologies;
        }
        public async Task<Technology> GetTechnologyByIdAsync(string techId)
        {
            try
            {
                Technology tech = await (from e in ctx.Technologies where e.TechId == techId select e).FirstAsync();
                return tech;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task InsertTechnologyAsync(Technology technology)
        {
            await ctx.Technologies.AddAsync(technology);
            await ctx.SaveChangesAsync();
        }
        public async Task UpdateTechnologyAsync(string techId, Technology technology)
        {
            Technology tech = await GetTechnologyByIdAsync(techId);
            tech.Duration = technology.Duration;
            tech.TechLevel = technology.TechLevel;
            tech.TechTitle = technology.TechTitle;
            await ctx.SaveChangesAsync();
        }
    }
}
