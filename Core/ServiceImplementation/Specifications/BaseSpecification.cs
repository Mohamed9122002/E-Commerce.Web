using DomainLayer.Contracts;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplementation.Specifications
{
    public abstract class BaseSpecification<TEntity, TKey> : ISpecification<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        protected BaseSpecification(Expression<Func<TEntity, bool>>? WhereExpression)
        {
            WhereExpressions = WhereExpression;
        }
        #region Include and Where 
        public Expression<Func<TEntity, bool>>? WhereExpressions { get; private set; }

        public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; } = [];
        protected void AddInclude(Expression<Func<TEntity, object>> includeExpression)
        {
            IncludeExpressions.Add(includeExpression);
        }

        #endregion
        #region OrderBy 

        public Expression<Func<TEntity, object>> OrderByExpressions { get; private set; }

        public Expression<Func<TEntity, object>> OrderBysDesExpressions { get; private set; }


        protected void AddOrderBy(Expression<Func<TEntity, object>> orderByExp )
        {
            OrderByExpressions = orderByExp;
        }
        protected void AddOrderByDescending(Expression<Func<TEntity, object>> orderByExpDesc)
        {
            OrderBysDesExpressions = orderByExpDesc;
        }

        #endregion
        #region Pagination


        public int Take { get; private set; }

        public int Skip { get; private set; }

        public bool IsPagingEnabled { get; set; }
       protected void ApplyPagination(int PageSize , int PageIndex)
        {
            IsPagingEnabled = true;
            Take = PageSize; 
            Skip = PageSize * (PageIndex - 1);
        }
        #endregion
    }
}
