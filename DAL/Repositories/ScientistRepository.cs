﻿using DAL.AdditionalModels;
using DAL.EF;
using DAL.Models;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;

namespace DAL.Repositories
{
    public class ScientistRepository : Repository<Scientist>, IRepository<Scientist>, IScientistRepository
    {
        public ScientistRepository(ParserDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Scientist> GetAsync(int id)
        {
            return await GetAll().FirstOrDefaultAsync(scientist => scientist.Id == id);
        }

        public async Task<Scientist> GetAsync(string name)
        {
            return await GetAll().FirstOrDefaultAsync(scientist => scientist.Name.Equals(name));
        }

        public async Task<List<Scientist>> GetScientistsListAsync(ScientistFilter? filter = null)
        {
            return await GetScientistsAsync(filter).ToListAsync();
        }

        public async Task<int> GetScientistsCountAsync(ScientistFilter? filter = null)
        {
            return await GetScientistsAsync(filter).CountAsync();
        }

        private IQueryable<Scientist> GetScientistsAsync(ScientistFilter? filter)
        {
            return GetAll()
                .Include(scientist => scientist.ScientistFieldsOfResearch)
                .Where(scientist => filter == null ||
                                    (string.IsNullOrEmpty(filter.Name) || (filter.Name.Contains(scientist.Name) ||
                                                                           scientist.Name.Contains(filter.Name))) &&
                                    (!filter.FieldOfResearchId.HasValue ||
                                     scientist.ScientistFieldsOfResearch.Any(scientistFieldsOfResearch =>
                                         scientistFieldsOfResearch.FieldOfResearchId == filter.FieldOfResearchId))
                );
        }

        public async Task<BlockingCollection<Scientist>> GetAllFromFieldOfResearch(List<ScientistFieldOfResearch> listOfScientistFieldOfResearches)
        {
            var result1 = new BlockingCollection<Scientist>();

            foreach (var res in listOfScientistFieldOfResearches)
            {

                var scientist = await GetAllWithIgnore().FirstOrDefaultAsync(e => e.Id == res.ScientistId);
                result1.Add(scientist);
            }

            return result1;
        }

        public async Task<BlockingCollection<Scientist>> GetAllFromWork(List<ScientistWork> getScientistWork)
        {
            var result = new BlockingCollection<Scientist>();

            foreach (var res in getScientistWork)
            {

                var scientist = await GetAllWithIgnore().FirstOrDefaultAsync(e => e.Id == res.ScientistId);
                result.Add(scientist);
            }

            return result;
        }
    }
}

