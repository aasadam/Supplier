using Domain.Core.Bus;
using FluentValidation.Results;
using SupplierReg.Domain.Commands.SupplierContracts.Validations;
using SupplierReg.Domain.Models.ValueObjects;
using System;
using System.Collections.Generic;

namespace SupplierReg.Domain.Commands.SupplierContracts
{
    public class AddSupplierCommand : Command
    {
        public AddSupplierCommand()
        {
            SupplierID = Guid.NewGuid();
        }

        public Guid SupplierID { get; set; }
        public Guid CompanyID { get; set; }
        public string Name { get; set; }
        public string CpfCnpj { get; set; }
        public DateTimeOffset? BirthDate { get; set; }
        public List<Phone> Phones { get; set; }
        public string Rg { get; set; }

        public override ValidationResult SetValidationResult()
            => (new AddSupplierValidations()).Validate(this);
    }
}
