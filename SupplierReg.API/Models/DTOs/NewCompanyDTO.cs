using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupplierReg.API.Models.DTOs
{
    public class NewCompanyDTO
    {
        public string UF { get; set; }
        public string Name { get; set; }
        public string CNPJ { get; set; }
    }
}
