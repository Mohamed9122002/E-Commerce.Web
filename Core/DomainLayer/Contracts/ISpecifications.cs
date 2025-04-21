using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Contracts
{
    public interface ISpecifications<TEntity ,Tkey> where TEntity : BaseEntity<Tkey>
    {
        // Property Signature For Each Dynamic Part int Query 
        public Expression<Func<TEntity,bool>> Criteria { get; }

        List<Expression<Func<TEntity, object>>> ExpressionIncludes { get; }


    }
}
