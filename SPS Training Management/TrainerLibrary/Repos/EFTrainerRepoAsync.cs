using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainerLibrary.Models;

namespace TrainerLibrary.Repos
{
    public class EFTrainerRepoAsync : ITrainerRepoAsync
    {
        SPSTrainerDBContext ctx = new SPSTrainerDBContext();
        public async Task DeleteTrainerAsync(string TrainerId)
        {
            Trainer trainer = await GetTrainerAsync(TrainerId);
            ctx.Trainers.Remove(trainer);
            await ctx.SaveChangesAsync();
        }

        public async Task<List<Trainer>> GetAllTrainersAsync()
        {
            List<Trainer> trainers = await ctx.Trainers.ToListAsync();
            return trainers;
        }

        public async Task<Trainer> GetTrainerAsync(string trainerId)
        {
            try
            {
                Trainer trainer = await (from t in ctx.Trainers where t.TrainerId == trainerId select t).FirstAsync();
                return trainer;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Trainer>> GetTrainersByTypeAsync(string type)
        {
            List<Trainer> trainers = await (from tr in ctx.Trainers where tr.TrainerType == type select tr).ToListAsync();
            return trainers;
        }

        public async Task InsertTrainerAsync(Trainer trainer)
        {
            await ctx.Trainers.AddAsync(trainer);
            await ctx.SaveChangesAsync();
        }

        public async Task UpdateTrainerAsync(string trainerId, Trainer trainer)
        {
            Trainer train = await GetTrainerAsync(trainerId);
            train.TrainerEmail = trainer.TrainerEmail;
            train.TrainerPhone = trainer.TrainerPhone;
            train.TrainerName = trainer.TrainerName;
            train.TrainerType = trainer.TrainerType;
            await ctx.SaveChangesAsync();
        }

    }
}
