﻿using Store.Data.Entities;
using Store.Repository.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository.Interfaces
{
    public interface IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public IQueryable<TEntity> GetAll();
        public Task<IEnumerable<TEntity>> GetAllWithSpecifications(ISpecification<TEntity> specs);
        public Task<TEntity> GetById(TKey id);
        public Task<TEntity> GetByIdWithSpecifications(ISpecification<TEntity> specs);
        public Task AddAsync(TEntity entity);
        public void UpdateAsync(TEntity entity);
        public void DeleteAsync(TEntity entity);
    }
}
