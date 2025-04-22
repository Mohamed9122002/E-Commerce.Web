using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Contracts
{
    public interface IGenericRepository<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        // GetAll
        Task<IEnumerable<TEntity>> GetAllAsync();
        // GetById
        Task<TEntity?> GetByIdAsync(Tkey id);
        #region With Specifications
        Task<TEntity?> GetByIdAsync(ISpecifications<TEntity, Tkey> specifications);
        Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity, Tkey> specifications);
        Task<int> CountAsync(ISpecifications<TEntity, Tkey> specifications);
        #endregion
        // Add 
        Task AddAsync(TEntity entity);
        // Update not version Async 
        void Update(TEntity entity);
        // Remove
        void Remove(TEntity entity);

    }
}
