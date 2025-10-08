using DomainLayer.Contracts;
using DomainLayer.Models;
using Persistence.Data.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class UnitOfWork(StoreDbContext _dbContext) : IUnitOfWork
    {
        private readonly Dictionary<string, object> _repositories = [];
        public IGenericRepository<TEntity, TKey> GenericRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
           // GetType Name 
           var TypeName = typeof(TEntity).Name;
            if (_repositories.ContainsKey(TypeName)){
                return (IGenericRepository<TEntity, TKey>)_repositories[TypeName];
            }else
            {
                // Create New Repository
                var CreateRepository = new GenericRepository<TEntity, TKey>(_dbContext);
                // Store Dictionary
                _repositories.Add(TypeName, CreateRepository);
                return CreateRepository;
            }
        }

        public async Task<int> SaveChangeAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
