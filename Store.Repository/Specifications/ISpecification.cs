using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository.Specifications
{
    public interface ISpecification<T>
    {
        // Criteria
        Expression<Func<T, bool>> Criteria { get; }
        // Includes
        List<Expression<Func<T, object>>> Includes { get; }
        Expression<Func<T, object>> OrderByAsc { get; }
        Expression<Func<T, object>> OrderByDesc { get; }

    }
}
