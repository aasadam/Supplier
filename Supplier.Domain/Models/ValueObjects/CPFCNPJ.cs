using Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SupplierReg.Domain.Models.ValueObjects
{
    public class CPFCNPJ : ValueObject<CPFCNPJ>
    {
        private string _cpfCnpj;

        private CPFCNPJ()
        {

        }

        public CPFCNPJ(string cpfCnpj)
        {
            _cpfCnpj = cpfCnpj;
        }        

        public static bool IsCpfOrCnpj(string cpfCnpj) => CPF.IsCpf(cpfCnpj) || CNPJ.IsCnpj(cpfCnpj);

        public static implicit operator string(CPFCNPJ cpfCnpj) => cpfCnpj._cpfCnpj;
        public static explicit operator CPFCNPJ(string cpfCnpj) => new CPFCNPJ(cpfCnpj);

        protected override bool EqualsHandle(CPFCNPJ other)
        {
            return this._cpfCnpj == other._cpfCnpj;
        }

        protected override int GetHashCodeHandle()
        {
            var hashCode = -455586387;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(_cpfCnpj);
            return hashCode;
        }

        public override string ToString()
            => _cpfCnpj;
    }
}
