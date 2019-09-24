using FluentValidation;
using SupplierReg.Domain.Models.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SupplierReg.Domain.Validations
{
    public class SupplierValidation<T> : AbstractValidator<T>
    {
        protected void ValidateName(Expression<Func<T, string>> nameFiled)
        {
            RuleFor(nameFiled).Cascade(CascadeMode.StopOnFirstFailure)
                                .NotNull().WithMessage("SupplierName was not provided.")
                                .NotEmpty().WithMessage("SupplierName was not provided.");
        }

        protected void ValidateCompanyID(Expression<Func<T, Guid>> companyIDFiled)
        {
            RuleFor(companyIDFiled).Cascade(CascadeMode.StopOnFirstFailure)
                                .NotNull().WithMessage("Company was not provided.")
                                .NotEmpty().WithMessage("Company was not provided.");
        }

        protected void ValidateCPFCNPJ(Expression<Func<T, string>> cpfcnpjFiled)
        {
            RuleFor(cpfcnpjFiled).Cascade(CascadeMode.StopOnFirstFailure)
                                .NotNull().WithMessage("CPF or CNPJ was not provided.")
                                .NotEmpty().WithMessage("CPF or CNPJ was not provided.")
                                .Must(cpfcnpj => CPFCNPJ.IsCpfOrCnpj(cpfcnpj)).WithMessage("Invalid CPF or CNPJ"); ;
        }

        protected void ValidatePrivateIndividual(Expression<Func<T, string>> cpfcnpjFiled,
                                                    Expression<Func<T, string>> rgFiled,
                                                    Expression<Func<T, DateTimeOffset?>> birthDateFiled)
        {
            When(supplier => CPF.IsCpf(cpfcnpjFiled.Compile().Invoke(supplier)), () =>
            {
                RuleFor(rgFiled).Cascade(CascadeMode.StopOnFirstFailure)
                                .NotNull().WithMessage("RG was not provided for private individual.")
                                .NotEmpty().WithMessage("RG was not provided for private individual.");

                RuleFor(birthDateFiled).Cascade(CascadeMode.StopOnFirstFailure)
                                .NotNull().WithMessage("Birth date was not provided for private individual.")
                                .NotEmpty().WithMessage("Birth date was not provided for private individual.");
            });
        }



    }
}
