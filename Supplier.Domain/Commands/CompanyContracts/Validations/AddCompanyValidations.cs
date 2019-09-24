using FluentValidation;
using SupplierReg.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SupplierReg.Domain.Commands.CompanyContracts.Validations
{
    public class AddCompanyValidations : CompanyValidation<AddCompanyCommand>
    {
        public AddCompanyValidations()
        {
            this.ValidateName(c => c.Name);
            this.ValidateCNPJ(c => c.CNPJ);            
            this.ValidateUF(c => c.UF);
        }
    }
}
