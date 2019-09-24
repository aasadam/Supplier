using Data.Core;
using Microsoft.EntityFrameworkCore;
using SupplierReg.Data.Context;
using SupplierReg.Domain.Models.Entities;
using SupplierReg.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplierReg.Data.Repositories
{
    public class SupplierRepository : Repository<Supplier>, ISupplierRepository
    {
        public SupplierRepository(SupplierContext context) : base(context)
        {
        }

        public async Task<bool> ExistsSupplierWithCPFCNPJ(string cpfcnpj)
        {
            return await _dbSet.AnyAsync(e => e.CPFCNPJ == cpfcnpj);
        }

        public async Task<IEnumerable<Supplier>> FilterAllSupliers(string name, string cpfcnpj, DateTimeOffset? creationdate)
        {
            IQueryable<Supplier> query = _dbSet.Include(e => e.Company);

            query = query.Where(e => string.IsNullOrWhiteSpace(name) || e.Name.ToLower().Contains(name.ToLower() ?? string.Empty));
            query = query.Where(e => string.IsNullOrWhiteSpace(cpfcnpj) || e.CPFCNPJ.ToString().Contains(cpfcnpj??string.Empty));
            query = query.Where(e => !creationdate.HasValue || e.CreationDate.Date == creationdate.Value);

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Supplier>> GetAllSupliers()
        {
            return await _dbSet.ToListAsync();
        }
    }
}
