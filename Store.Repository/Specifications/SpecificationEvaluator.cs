﻿using Microsoft.EntityFrameworkCore;
using Store.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository.Specifications
{
    public class SpecificationEvaluator<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> spec)
        {
            var query = inputQuery;

            if (spec.Criteria is not null)
            {
                query = query.Where(spec.Criteria);
            }

            if (spec.OrderByAsc is not null)
            {
                query = query.OrderBy(spec.OrderByAsc);
            }

            if (spec.OrderByDesc is not null)
            {
                query = query.OrderByDescending(spec.OrderByDesc);
            }

            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));

            return query;
        }
    }
}