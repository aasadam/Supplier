using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupplierReg.API.Models.DTOs
{
    public class PhoneDTO
    {
        public string PhoneNumber { get; set; }
        public bool IsResidential { get; set; }
    }
}
