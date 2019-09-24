using Domain.Core.Persistence;
using SupplierReg.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SupplierReg.Domain.Repositories
{
    public interface ICompanyRepository : IRepository<Company>
    {
        Task<IEnumerable<Company>> GetAllCompanies();
        Task<bool> ExistsCompanyWithCNPJ(string cnpj);
    }
}
