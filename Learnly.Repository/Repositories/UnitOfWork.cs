using Learnly.Core.Entities;
using Learnly.Core.Repositories.Contract;
using Learnly.Repository.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace Learnly.Repository.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _dbContext;
        //private Dictionary<string, GenericRepository<BaseEntity>> _repositories;
        private Hashtable _repositories;

        public UnitOfWork(StoreContext dbContext)
        {
            _dbContext = dbContext;
            _repositories = new Hashtable();
        }
        public async Task<int> CompleteAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await _dbContext.DisposeAsync();
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            var key = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(key))
            {
                var repository = new GenericRepository<TEntity>(_dbContext);
                _repositories.Add(key, repository);
            }

            return _repositories[key] as IGenericRepository<TEntity>;
        }
    }
}
