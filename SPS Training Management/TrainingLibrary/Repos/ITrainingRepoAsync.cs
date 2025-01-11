using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingLibrary.Models;

namespace TrainingLibrary.Repos
{
    public interface ITrainingRepoAsync
    {
        Task InsertTrainingAsync(Training training);
        Task UpdateTrainingAsync(string trainingId, Training training);
        Task DeleteTrainingAsync(string trainingId);
        Task<Training> GetTrainingAsync(string trainingId);
        Task<List<Training>> GetAllTrainingsAsync();
        Task InsertTrainerAsync(Trainer trainer);
        Task InsertTechnologyAsync(Technology technology);
        Task<List<Training>> GetTrainingsByTechnologyAsync(string technologyId);
        Task<List<Training>> GetTrainingsByTrainerAsync(string trainerId);
    }
}
