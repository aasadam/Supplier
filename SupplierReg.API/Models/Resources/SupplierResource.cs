using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupplierReg.API.Models.Resources
{
    public class SupplierResource
    {
        public string Name { get; set; }
        public string CPFCNPJ { get; set; }
        public DateTimeOffset CreationDate { get; set; }
        public string CompanyName { get; set; }
    }
}
