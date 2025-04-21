using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    static class SpecificationEvaluator
    {
        // Create Query
        // 
        public static IQueryable<TEntity> CreateQuery<TEntity, Tkey>(IQueryable<TEntity> InputQuery, ISpecifications<TEntity, Tkey> specifications) where TEntity : BaseEntity<Tkey>

        {
            var Query = InputQuery;
            if (specifications.Criteria != null)
            {
                Query = Query.Where(specifications.Criteria);
            }
            // Order By 
            if (specifications.OrderBy != null)
            {
                Query = Query.OrderBy(specifications.OrderBy);
            }
            else if (specifications.OrderyByDescending != null)
            {
                Query = Query.OrderByDescending(specifications.OrderyByDescending);
            }
            if (specifications.ExpressionIncludes is not null && specifications.ExpressionIncludes.Count > 0)
            {
                Query = specifications.ExpressionIncludes.Aggregate(Query, (CurrentQuery, IncludeExperssion) => CurrentQuery.Include(IncludeExperssion));

            }
            // Pagination
            if (specifications.IsPagingEnabled)
            {
                Query = Query.Skip(specifications.Skip).Take(specifications.Take);
            }
            return Query;
        }
    }
}
