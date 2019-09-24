using SupplierReg.Data.Context;
using SupplierReg.Domain.Models.Entities;
using SupplierReg.Domain.Models.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplierReg.CrossCutting.IOC
{
    public static class SeedDatabaseTest
    {
        public async static Task SeedSupplierRegDB(SupplierContext supplierContext)
        {
            if (supplierContext.Companies.Any())
                return;

            var company = new Company("CompanyA", new UF("SC"), new CNPJ("83841875000143"), new Guid("0822f500-edb6-400b-856d-b01d4a67695d"));

            supplierContext.Companies.Add(company);
            supplierContext.Companies.Add(new Company("CompanyB", new UF("RJ"), new CNPJ("38389185000140"), Guid.NewGuid()));
            supplierContext.Companies.Add(new Company("CompanyC", new UF("SP"), new CNPJ("27569276000141"), Guid.NewGuid()));
            supplierContext.Companies.Add(new Company("CompanyPR", new UF("PR"), new CNPJ("84730514000192"), new Guid("0b1b65bb-d799-47c4-876f-aafc4ae0b93e")));

            await supplierContext.SaveChangesAsync();

            supplierContext.Suppliers.Add(new Supplier("SupplierA", new CPFCNPJ("81777787000102"), null, company, Guid.NewGuid()));
            supplierContext.Suppliers.Add(new Supplier("SupplierB", new CPFCNPJ("95582539000102"), null, company, Guid.NewGuid()));

            await supplierContext.SaveChangesAsync();
        }
    }
}
