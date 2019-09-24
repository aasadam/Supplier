using Data.Core;
using Microsoft.EntityFrameworkCore;
using SupplierReg.Data.Context;
using SupplierReg.Domain.Models.Entities;
using SupplierReg.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SupplierReg.Data.Repositories
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        public CompanyRepository(SupplierContext context) : base(context)
        {
        }

        public async Task<bool> ExistsCompanyWithCNPJ(string cnpj)
        {
            return await _dbSet.AnyAsync(e => e.CNPJ == cnpj);
        }

        public async Task<IEnumerable<Company>> GetAllCompanies()
        {
            return await _dbSet.ToListAsync();
        }
    }
}
