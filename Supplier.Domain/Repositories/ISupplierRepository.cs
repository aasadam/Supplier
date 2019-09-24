using Domain.Core.Persistence;
using SupplierReg.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SupplierReg.Domain.Repositories
{
    public interface ISupplierRepository : IRepository<Supplier>
    {
        Task<IEnumerable<Supplier>> GetAllSupliers();
        Task<bool> ExistsSupplierWithCPFCNPJ(string cpfcnpj);
        Task<IEnumerable<Supplier>> FilterAllSupliers(string name, string cpfcnpj, DateTimeOffset? creationdate);
    }
}
