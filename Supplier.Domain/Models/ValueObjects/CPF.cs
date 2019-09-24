using Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SupplierReg.Domain.Models.ValueObjects
{
    public class CPF : ValueObject<CPF>
    {
        private string _cpf;

        private CPF()
        {

        }

        public CPF(string cpf)
        {
            _cpf = cpf;
        }                

        //Not my code: http://www.macoratti.net/11/09/c_val1.htm
        public static bool IsCpf(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf)) return false;
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }

        public static implicit operator string(CPF cpf) => cpf._cpf;
        public static explicit operator CPF(string cpf) => new CPF(cpf);

        protected override bool EqualsHandle(CPF other)
        {
            return this._cpf == other._cpf;
        }

        protected override int GetHashCodeHandle()
        {
            var hashCode = -455586387;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(_cpf);
            return hashCode;
        }

        public override string ToString()
            => _cpf;
    }
}
