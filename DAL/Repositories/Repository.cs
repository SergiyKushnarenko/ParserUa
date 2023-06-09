﻿using DAL.Repositories.Interfaces;

namespace DAL.Repositories
{
    public abstract class Repository<EntType> : IRepository<EntType> where EntType : class
    {
        private readonly ParserDbContext _context;

        public Repository(ParserDbContext context)
        {
            _context = context;
        }

        public IQueryable<EntType> GetAll()
        {
            return _context.Set<EntType>().AsQueryable();
        }

        public Task<int> CreateAsync(EntType entity)
        {
            _context.Add(entity);
            return _context.SaveChangesAsync();
        }

        public Task<int> CreateAsync(IEnumerable<EntType> entities)
        {
            _context.AddRange(entities);
            return _context.SaveChangesAsync();
        }

        public Task<int> UpdateAsync(EntType entity)
        {
            _context.Update(entity);
            return _context.SaveChangesAsync();
        }

        public Task<int> UpdateAsync(IEnumerable<EntType> entities)
        {
            _context.UpdateRange(entities);
            return _context.SaveChangesAsync();
        }

        public Task<int> DeleteAsync(EntType entity)
        {
            _context.Remove(entity);
            return _context.SaveChangesAsync();
        }

        public Task<int> DeleteAsync(IEnumerable<EntType> entities)
        {
            _context.RemoveRange(entities);
            return _context.SaveChangesAsync();
        }
    }
}
