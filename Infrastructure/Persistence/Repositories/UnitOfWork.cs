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
        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            // get Type Name 
            var typeName = typeof(TEntity).Name;
            // Dict<string,object> => string key [Name of Type ] 
            //-- object value [Repository]
            // check if the type is already in the dictionary
            //if (_repositories.ContainsKey(typeName))
            //{
            //    return (IGenericRepository<TEntity, TKey>)_repositories[typeName];
            //}
            if (_repositories.TryGetValue(typeName ,out object? value ))
            { 
                // get Value to insert value inside varibale 
                return (IGenericRepository<TEntity, TKey>)value;
            }
            else
            {
                // create a new object  of the repository
                var repositoryType = new GenericRepository<TEntity, TKey>(_dbContext);
                // Store object in the dictionary
                _repositories.Add(typeName, repositoryType);
                // Return object 
                return repositoryType;
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
