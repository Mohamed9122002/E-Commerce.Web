using DomainLayer.Contracts;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplemention.Specifications
{
    public class BaseSpecifications<TEntity, Tkey> : ISpecifications<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        public BaseSpecifications(Expression<Func<TEntity, bool>> criteria)
        {
            Criteria = criteria;
        }
        public Expression<Func<TEntity, bool>> Criteria { get; private set; }
        #region Includes

        public List<Expression<Func<TEntity, object>>> ExpressionIncludes { get; } = [];

        // AddInclude 
        public void AddInclude(Expression<Func<TEntity, object>> includeExpression)
        {
            ExpressionIncludes.Add(includeExpression);
        }
        #endregion
        #region Sorting
        public Expression<Func<TEntity, object>> OrderBy { get; private set; }

        public Expression<Func<TEntity, object>> OrderyByDescending { get; private set; }


        // Add OrderBy
        public void AddOrderBy(Expression<Func<TEntity, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }
        // Add OrderyByDescending
        public void AddOrderyByDescending(Expression<Func<TEntity, object>> orderyByDescendingExpression)
        {
            OrderyByDescending = orderyByDescendingExpression;
        }
        #endregion
        #region Pagination
        public int Take { get; private set; }

        public int Skip { get; private set; }

        public bool IsPagingEnabled { get; set; }
        // Add Pagination
        protected void ApplyPagination(int PageSize , int PageIndex)
        {
            IsPagingEnabled = true;
            Take = PageSize;
            Skip = PageSize * (PageIndex - 1);

        }

        #endregion


    }
}
