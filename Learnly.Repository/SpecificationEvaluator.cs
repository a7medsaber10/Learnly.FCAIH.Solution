using Learnly.Core.Entities;
using Learnly.Core.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learnly.Repository
{
    public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> innerQuery, ISpecification<TEntity> specification) 
        {
            // _dbcontext.Set<T>
            var query = innerQuery;

            if (specification.Criteria != null) 
            {
                // _dbcontext.Set<T>.Where(criteria)
                query = query.Where(specification.Criteria);
            }


            if(specification.OrderBy != null)
            {  
                query = query.OrderBy(specification.OrderBy);
            }
            else if(specification.OrderByDescending != null) 
            {
                query = query.OrderByDescending(specification.OrderByDescending);
            }

            if (specification.IsPaginationEnabled)
            {
                query = query.Skip(specification.Skip).Take(specification.Take);
            }

            query = specification.Includes.Aggregate(query,(currentQuery, IncludesExpression) => currentQuery.Include(IncludesExpression));

            return query;
        }
    }
}
