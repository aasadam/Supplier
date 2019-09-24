using Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SupplierReg.Domain.Models.ValueObjects
{
    public class CNPJ : ValueObject<CNPJ>
    {
        private string _cnpj;

        private CNPJ()
        {

        }

        public CNPJ(string cnpj)
        {
            _cnpj = cnpj;
        }                

        //Not my code: http://www.macoratti.net/11/09/c_val1.htm
        public static bool IsCnpj(string cnpj)
        {
            if (string.IsNullOrWhiteSpace(cnpj)) return false;
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCnpj;
            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
            if (cnpj.Length != 14)
                return false;
            tempCnpj = cnpj.Substring(0, 12);
            soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cnpj.EndsWith(digito);
        }

        public static implicit operator string(CNPJ cnpj) => cnpj._cnpj;
        public static explicit operator CNPJ(string cnpj) => new CNPJ(cnpj);


        protected override bool EqualsHandle(CNPJ other)
        {
            return this._cnpj == other._cnpj;
        }

        protected override int GetHashCodeHandle()
        {
            var hashCode = -455586387;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(_cnpj);
            return hashCode;
        }

        public override string ToString()
            => _cnpj;
    }
}
