using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnologyLibrary.Models;

namespace TechnologyLibrary.Repos
{
    public interface ITechnologyRepoAsync
    {
        Task InsertTechnologyAsync(Technology technology);
        Task UpdateTechnologyAsync(string techId, Technology technology);
        Task DeleteTechnologyAsync(string techId);
        Task<Technology> GetTechnologyByIdAsync(string techId);
        Task<List<Technology>> GetAllTechnologies();
        Task<List<Technology>> GetTechnologiesByLevelAsync(string level);

    }
}
