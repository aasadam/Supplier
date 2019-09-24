using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace Data.Core
{
    public abstract class DbContextBase : DbContext
    {
        public DbContextBase( DbContextOptions options) : base(options)
        {
        }

        public void DetachAllEntities()
        {
            this.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added ||
                            e.State == EntityState.Modified ||
                            e.State == EntityState.Deleted)
                .ToList()
                .ForEach((entry) => entry.State = EntityState.Detached);
        }
    }
}
