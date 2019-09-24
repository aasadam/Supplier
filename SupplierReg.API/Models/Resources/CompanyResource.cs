using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupplierReg.API.Models.Resources
{
    public class CompanyResource
    {
        public Guid CompanyID { get; set; }
        public string Name { get; set; }
        public string UF { get; set; }
        public string CNPJ { get; set; }
    }
}
