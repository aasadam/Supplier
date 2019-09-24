using Domain.Core.Models.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SupplierReg.Domain.Models.Entities;
using SupplierReg.Domain.Models.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace SupplierReg.Data.Mappings
{
    public class SupplierMapping : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.Property(e => e.Id);
            builder.Property(e => e.Name).IsRequired();
            builder.OwnsOne<CPFCNPJ>(e => e.CPFCNPJ, o =>
            {
                o.Property<string>("_cpfCnpj").HasColumnName("CPFCNPJ").IsRequired().HasMaxLength(14);
            });
            builder.Property(e => e.BirthDate);
            builder.Property(e => e.CreationDate).IsRequired().ValueGeneratedOnAdd();
            builder.OwnsOne<RG>(e => e.RG, o =>
            {
                o.Property<string>("_rg").HasColumnName("RG");
            });
            builder.OwnsMany<Phone>(e => e.Phones, om =>
            {
                om.ToTable("SupplierPhone");
                
                om.Property<Guid>("PhoneId").ValueGeneratedOnAdd();
                om.Property<Guid>("SupplierId");
                om.HasKey("PhoneId");
                om.HasForeignKey("SupplierId");
                om.Property(p => p.CountryCode);
                om.Property(p => p.StateCode);
                om.Property(p => p.PhoneNumber);
            });
            builder.HasOne(e => e.Company).WithMany(f => f.Suppliers).IsRequired().HasForeignKey("CompanyId").OnDelete(DeleteBehavior.Restrict);
        }
    }
}
