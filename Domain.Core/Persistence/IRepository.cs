using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Core.Persistence
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        void Add(TEntity obj);
        TEntity Find(Guid id);
        IQueryable<TEntity> All();
        void Update(TEntity obj);
        void Remove(Guid id);
        bool SaveChanges();
        void Rollback();
    }
}
