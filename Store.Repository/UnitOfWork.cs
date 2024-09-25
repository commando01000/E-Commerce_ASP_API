using Store.Data.Contexts;
using Store.Data.Entities;
using Store.Repository.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private StoreDBContext _context;
        private Hashtable _repositories;
        public UnitOfWork(StoreDBContext context)
        {
            this._context = context;
        }

        public IGenericRepository<TEntity, TKey> Repository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            var EntityKey = typeof(TEntity).Name;
            if (_repositories is null)
            {
                _repositories = new Hashtable();
                if (!_repositories.ContainsKey(typeof(TEntity).Name))
                {
                    var RepoType = typeof(GenericRepository<,>);
                    var RepoInstance = Activator.CreateInstance(RepoType.MakeGenericType(typeof(TEntity), typeof(TKey)), _context);
                    _repositories.Add(typeof(TEntity).Name, RepoInstance);
                }
            }
            else
            {
                if (!_repositories.ContainsKey(typeof(TEntity).Name))
                {
                    var RepoType = typeof(GenericRepository<,>);
                    var RepoInstance = Activator.CreateInstance(RepoType.MakeGenericType(typeof(TEntity), typeof(TKey)), _context);
                    _repositories.Add(typeof(TEntity).Name, RepoInstance);
                }
            }
            return (IGenericRepository<TEntity, TKey>)_repositories[EntityKey];
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
