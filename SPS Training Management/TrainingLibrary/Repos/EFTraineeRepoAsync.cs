using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Json;
using TrainingLibrary.Models;

namespace TrainingLibrary.Repos
{
    public class EFTraineeRepoAsync : ITraineeRepoAsync
    {
        SPSTrainingDBContext ctx = new SPSTrainingDBContext();

        public async Task DeleteTraineeAsync(string trainingId, string traineeId)
        {
            Trainee trainee = await GetTraineeByIdAsync(trainingId, traineeId);
            ctx.Trainees.Remove(trainee);
            await ctx.SaveChangesAsync();
        }

        public async Task<List<Trainee>> GetAllTraineesAsync()
        {
            List<Trainee> trainees = await ctx.Trainees.ToListAsync();
            return trainees;
        }

        public async Task<Trainee> GetTraineeByIdAsync(string  trainingId, string traineeId)
        {
            try
            {
                Trainee trainee = await (from tra in ctx.Trainees where tra.TrainingId == trainingId && traineeId == tra.TraineeId  select tra).FirstAsync();
                return trainee;
            }
            catch (Exception ex)
            {
                throw new Exception("Enter valid TrainingId and TraineeId");
            }
        }

        public async Task<List<Trainee>> GetTraineesByTraining(string trainingId)
        {
            List<Trainee> trainees = await (from tr in ctx.Trainees where tr.TrainingId == trainingId  select tr).ToListAsync();
            if (trainees.Count == 0)
                throw new Exception("No such training id");
            else
                return trainees;
        }

        public async Task<List<Trainee>> GetTrainingsByTrainee(string traineeId)
        {
            List<Trainee> trainees = await (from tr in ctx.Trainees where tr.TraineeId == traineeId select tr).ToListAsync();
            if (trainees.Count == 0)
                throw new Exception("No such trainee id");
            else
                return trainees;
        }

        public async Task InsertEmployeeAsync(Employee employee)
        {
            await ctx.Employees.AddAsync(employee);
            await ctx.SaveChangesAsync();
        }

        public async Task InsertTraineeAsync(Trainee trainee)
        {
            await ctx.Trainees.AddAsync(trainee);
            await ctx.SaveChangesAsync();
        }

        public async Task UpdateTraineeAsync(string trainingId, string traineeId, Trainee trainee)
        {
            Trainee trainee1 = await GetTraineeByIdAsync(trainingId, traineeId);
            trainee1.TraineeStatus = trainee.TraineeStatus;
            trainee1.Remarks = trainee.Remarks;
            await ctx.SaveChangesAsync();
        }

    }
}
