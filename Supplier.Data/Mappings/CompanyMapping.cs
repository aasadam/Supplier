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
    public class CompanyMapping : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.Property(e => e.Id);
            builder.Property(e => e.Name).IsRequired();
            builder.OwnsOne<UF>(e => e.UF, o =>
            {
                o.Property<string>("_uf").HasColumnName("UF").IsRequired();
            });
            builder.OwnsOne<CNPJ>(e => e.CNPJ, o =>
            {
                o.Property<string>("_cnpj").HasColumnName("CNPJ").IsRequired();
            });


            builder.HasMany<Supplier>(e => e.Suppliers).WithOne(f => f.Company).IsRequired().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
