using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupplierReg.API.Models.DTOs
{
    public class ApiErrorDTO
    {
        public string Key { get; set; }

        public string Message { get; set; }

        public string Detail { get; set; }
    }
}
