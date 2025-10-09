using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    static class SpecificationsEvaluator
    {

        // Create Query 
        public static IQueryable<TEntity>CreateQuery<TEntity,TKey>(IQueryable<TEntity> entities, ISpecification<TEntity, TKey> specifications) where TEntity : BaseEntity<TKey>
        {
            var Query = entities;
            if (specifications.WhereExpressions is not null)
            {
                Query = Query.Where(specifications.WhereExpressions);
            }
            if (specifications.OrderByExpressions is not null)
            {
                Query = Query.OrderBy(specifications.OrderByExpressions);
            }
            if (specifications.OrderBysDesExpressions is not null)
            {
                Query = Query.OrderByDescending(specifications.OrderBysDesExpressions);
            }
            if (specifications.IncludeExpressions is not null &&specifications.IncludeExpressions.Count > 0)
            {
                Query = specifications.IncludeExpressions.Aggregate(Query, (CurrentQuery, IncludeEx) => CurrentQuery.Include(IncludeEx));
            }
            return Query;
        }
    }
}
