using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupplierReg.API.Models.DTOs
{
    public class NewSupplierDTO
    {
        public Guid CompanyID { get; set; }
        public string Name { get; set; }
        public string CpfCnpj { get; set; }
        public DateTimeOffset? BirthDate { get; set; }
        public List<PhoneDTO> Phones { get; set; }
        public string Rg { get; set; }
    }
}
