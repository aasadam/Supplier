using Data.Core;
using Microsoft.EntityFrameworkCore;
using SupplierReg.Data.Mappings;
using SupplierReg.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SupplierReg.Data.Context
{
    public class SupplierContext : DbContextBase
    {
        public SupplierContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CompanyMapping());
            modelBuilder.ApplyConfiguration(new SupplierMapping());

            base.OnModelCreating(modelBuilder);
        }
    }
}
