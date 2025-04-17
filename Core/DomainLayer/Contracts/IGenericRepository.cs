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
        // Add 
        Task AddAsync(TEntity entity);
        // Update not version Async 
        void Update(TEntity entity);
        // Remove
        void Remove(TEntity entity);

    }
}
