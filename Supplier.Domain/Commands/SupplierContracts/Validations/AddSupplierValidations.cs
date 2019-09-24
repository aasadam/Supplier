using SupplierReg.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SupplierReg.Domain.Commands.SupplierContracts.Validations
{
    public class AddSupplierValidations : SupplierValidation<AddSupplierCommand>
    {
        public AddSupplierValidations()
        {
            ValidatePrivateIndividual(  x => x.CpfCnpj,
                                        x => x.Rg,
                                        x => x.BirthDate);
            ValidateCompanyID(x => x.CompanyID);
            ValidateName(x => x.Name);
            ValidateCPFCNPJ(x => x.CpfCnpj);
        }
    }
}
