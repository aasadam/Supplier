using System;
using System.Collections.Generic;
using Domain.Core.Models;
using SupplierReg.Domain.Models.ValueObjects;

namespace SupplierReg.Domain.Models.Entities
{
    public class Supplier : Entity
    {
        public Supplier(string name, CPFCNPJ cPFCNPJ, ICollection<Phone> phones, Company company, Guid id, DateTimeOffset? birthDate = null, RG rg = null)
        {
            Id = id;
            Name = name;
            CPFCNPJ = cPFCNPJ;           
            CreationDate = DateTimeOffset.Now;
            Phones = phones;
            Company = company;

            if (CPF.IsCpf(cPFCNPJ))
            {
                BirthDate = birthDate;
                RG = rg;
                if(BirthDate == null || RG == null)
                {
                    throw new InvalidOperationException("Birthdate and RG must be provided for private individual");
                }
            }
        }

        private Supplier()
        {
            Phones = new List<Phone>();
        }

        public string Name { get; set; }
        public CPFCNPJ CPFCNPJ { get; set; }
        public RG RG { get; set; }
        public DateTimeOffset? BirthDate { get; set; }
        public DateTimeOffset CreationDate { get; set; }
        public ICollection<Phone> Phones { get; set; }

        public Company Company { get; set; }
    }
}
