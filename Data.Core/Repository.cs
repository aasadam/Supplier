using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Core.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Data.Core
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected virtual DbContextBase _context { get; private set; }
        protected readonly DbSet<TEntity> _dbSet;

        public Repository(DbContextBase context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public virtual void Add(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public virtual void Add(IEnumerable<TEntity> entities)
        {
            _dbSet.AddRange(entities);
        }

        public virtual TEntity Find(Guid id)
        {
            return _dbSet.Find(id);
        }

        public virtual IQueryable<TEntity> All()
        {
            return _dbSet;
        }

        public virtual void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        public virtual void Update(IEnumerable<TEntity> entities)
        {
            _dbSet.UpdateRange(entities);
        }

        public virtual void Remove(Guid id)
        {
            _dbSet.Remove(_dbSet.Find(id));
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public void Rollback()
        {
            _context.DetachAllEntities();
        }
    }
}
