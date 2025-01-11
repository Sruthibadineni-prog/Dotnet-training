using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingLibrary.Models;

namespace TrainingLibrary.Repos
{
    public interface ITraineeRepoAsync
    {
        Task InsertTraineeAsync(Trainee trainee);
        Task UpdateTraineeAsync(string trainingId, string traineeId, Trainee trainee);
        Task DeleteTraineeAsync(string trainingId, string traineeId);
        Task<Trainee> GetTraineeByIdAsync(string trainingId, string traineeId);
        Task<List<Trainee>> GetAllTraineesAsync();
        Task<List<Trainee>> GetTraineesByTraining(string trainingId);
        Task<List<Trainee>> GetTrainingsByTrainee(string traineeId);
        Task InsertEmployeeAsync(Employee employee);

    }
}
