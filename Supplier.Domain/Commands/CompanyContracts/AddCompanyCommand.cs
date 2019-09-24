using Domain.Core.Bus;
using Domain.Core.Models.ValueObjects;
using FluentValidation.Results;
using SupplierReg.Domain.Commands.CompanyContracts.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SupplierReg.Domain.Commands.CompanyContracts
{
    public class AddCompanyCommand : Command
    {
        public AddCompanyCommand()
        {
            CompanyID = Guid.NewGuid();
        }

        public Guid CompanyID { get; set; }
        public string UF { get; set; }
        public string Name { get; set; }
        public string CNPJ { get; set; }

        public override ValidationResult SetValidationResult()
            => (new AddCompanyValidations()).Validate(this);
    }
}
