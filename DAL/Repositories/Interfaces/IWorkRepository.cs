﻿using DAL.Models;

namespace DAL.Repositories.Interfaces
{
    public interface IWorkRepository : IRepository<Work>
    {
        Task<int> GetCountAsync();
        Task<Work> GetAsync(int id);

        Task<Work> GetAsync(string title);

        // Task<List<Work>> GetFieldsOfResearchAsync(FieldOfResearchFilter? filter = null);
    }
}
