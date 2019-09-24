using Domain.Core.Models;
using SupplierReg.Domain.Models.ValueObjects;
using System;
using System.Collections.Generic;

namespace SupplierReg.Domain.Models.Entities
{
    public class Company : Entity
    {
        public Company(string name, UF uF, CNPJ cNPJ, Guid id)
        {
            Id = id;
            Name = name;
            UF = uF;
            CNPJ = cNPJ;
            Suppliers = new List<Supplier>();
        }

        private Company()
        {
            Suppliers = new List<Supplier>();
        }

        public string Name { get; set; }
        public UF UF { get; set; }
        public CNPJ CNPJ { get; set; }

        public ICollection<Supplier> Suppliers { get; set; }
    }
}
