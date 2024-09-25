using Store.Data.Entities;
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
        public Task<TEntity> GetById(TKey id);
        public Task AddAsync(TEntity entity);
        public void UpdateAsync(TEntity entity);
        public void DeleteAsync(TEntity entity);
    }
}
