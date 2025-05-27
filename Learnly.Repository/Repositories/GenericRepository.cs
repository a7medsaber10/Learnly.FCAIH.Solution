using Learnly.Core.Entities;
using Learnly.Core.Repositories.Contract;
using Learnly.Core.Specifications;
using Learnly.Repository.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learnly.Repository.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _dbContext;

        public GenericRepository(StoreContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            // Temprorily we will use this solution until we use specification design pattern to include related data (department, category) 
            if (typeof(T) == typeof(Course))
            {
                // unsafe casting // 
                return (IReadOnlyList<T>)await _dbContext.Set<Course>().Include(c => c.Category).Include(c => c.Department).ToListAsync();
            }

            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecifications(spec).ToListAsync();
        }

        public async Task<T?> GetAsync(int id)
        {
            // Temprorily we will use this solution until we use specification design pattern to include related data (department, category) 5
            if (typeof(T) == typeof(Course))
            {
                // unsafe casting // 
                return await _dbContext.Set<Course>().Where(C => C.Id == id).Include(c => c.Category).Include(c => c.Department).FirstOrDefaultAsync() as T;
            }

            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<int> GetCountAsync(ISpecification<T> spec)
        {
            return await ApplySpecifications(spec).CountAsync();
        }

        public async Task<T?> GetWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecifications(spec).FirstOrDefaultAsync();
        }

        private IQueryable<T> ApplySpecifications(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_dbContext.Set<T>(), spec);
        }


    }
}
