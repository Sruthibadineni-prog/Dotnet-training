using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingLibrary.Models;

namespace TrainingLibrary.Repos
{
    public class EFTrainingRepoAsync : ITrainingRepoAsync
    {
        SPSTrainingDBContext ctx = new SPSTrainingDBContext();
        public async Task DeleteTrainingAsync(string trainingId)
        {
            Training training = await GetTrainingAsync(trainingId);
            await ctx.SaveChangesAsync();
        }
        public async Task<List<Training>> GetAllTrainingsAsync()
        {
            List<Training> trainingList = await ctx.Training.ToListAsync();
            return trainingList;
        }
        public async Task<Training> GetTrainingAsync(string trainingId)
        {
            try
            {
                Training training = await (from tra in ctx.Training where trainingId == tra.TrainingId select tra).FirstAsync();
                return training;
            }
            catch (Exception ex)
            {
                throw new Exception("Enter a valid trainingId");
            }
        }

        public async Task<List<Training>> GetTrainingsByTechnologyAsync(string technologyId)
        {
            List<Training> trainings = await (from trn in ctx.Training where trn.TechId == technologyId  select trn).ToListAsync();
            if (trainings.Count == 0)
                throw new Exception("No training in this technology");
            else
                return trainings;
        }

        public async Task<List<Training>> GetTrainingsByTrainerAsync(string trainerId)
        {
            List<Training> trainings = await (from trn in ctx.Training where trn.TrainerId == trainerId  select trn).ToListAsync();
            if (trainings.Count == 0)
                throw new Exception("No training for this trainer");
            else
                return trainings;
        }

        public async Task InsertTechnologyAsync(Technology technology)
        {
            await ctx.Technologies.AddAsync(technology);
            await ctx.SaveChangesAsync();
        }

        public async Task InsertTrainerAsync(Trainer trainer)
        {
            await ctx.Trainers.AddAsync(trainer);
            await ctx.SaveChangesAsync();
        }

        public async Task InsertTrainingAsync(Training training)
        {
            await ctx.Training.AddAsync(training);
            await ctx.SaveChangesAsync();
        }

        public async Task UpdateTrainingAsync(string trainingId, Training training)
        {
            Training training1 = await GetTrainingAsync(trainingId);
            training1.StartDate = training.StartDate;
            training1.EndDate = training.EndDate;
            await ctx.SaveChangesAsync();
        }

    }
}
