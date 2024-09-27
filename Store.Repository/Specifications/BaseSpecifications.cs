using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository.Specifications
{
    public class BaseSpecifications<T> : ISpecification<T>
    {
        public BaseSpecifications(Expression<Func<T, bool>> criteria)
        {
            this.Criteria = criteria;
        }
        public Expression<Func<T, bool>> Criteria { get; }

        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();

        public Expression<Func<T, object>> OrderByAsc { get; private set; }

        public Expression<Func<T, object>> OrderByDesc { get; private set; }

        protected void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            this.Includes.Add(includeExpression);
        }
        protected void AddOrderByAsc(Expression<Func<T, object>> orderByExpression)
        {
            this.OrderByAsc = orderByExpression;
        }
        protected void AddOrderByDesc(Expression<Func<T, object>> orderByExpression)
        {
            this.OrderByDesc = orderByExpression;
        }
    }
}
