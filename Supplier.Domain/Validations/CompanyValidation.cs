using FluentValidation;
using System;
using System.Linq.Expressions;
using SupplierReg.Domain.Models.ValueObjects;

namespace SupplierReg.Domain.Validations
{
    public class CompanyValidation<T> : AbstractValidator<T>
    {
        protected void ValidateName(Expression<Func<T,string>> nameFiled)
        {
            RuleFor(nameFiled).Cascade(CascadeMode.StopOnFirstFailure)
                                .NotNull().WithMessage("Company name was not provided.")
                                .NotEmpty().WithMessage("Company name was not provided.");
        }

        protected void ValidateUF(Expression<Func<T,string>> ufField)
        {
            RuleFor(ufField).Cascade(CascadeMode.StopOnFirstFailure)
                            .NotNull().WithMessage("UF was not provided.")
                            .NotEmpty().WithMessage("UF was not provided.")
                            .Must(uf => UF.IsUF(uf)).WithMessage("Invalid UF");
        }

        protected void ValidateCNPJ(Expression<Func<T,string>> cnpjField)
        {
            RuleFor(cnpjField).Cascade(CascadeMode.StopOnFirstFailure)
                            .NotNull().WithMessage("CNPJ was not provided.")
                            .NotEmpty().WithMessage("CNPJ was not provided.")                            
                            .Must(cnpj => CNPJ.IsCnpj(cnpj)).WithMessage("Invalid CNPJ");
        }
    }
}
