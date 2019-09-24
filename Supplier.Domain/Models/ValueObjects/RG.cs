using Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SupplierReg.Domain.Models.ValueObjects
{
    public class RG : ValueObject<RG>
    {
        private string _rg;

        private RG()
        {

        }

        public RG(string rg)
        {
            _rg = rg;
        }

        public static bool IsRG(string rg)
        {
            return !string.IsNullOrWhiteSpace(rg) && !rg.Any(c => c >= '0' && c <= '9');
        }

        public static implicit operator string(RG rg) => rg._rg;
        public static explicit operator RG(string rg) => new RG(rg);

        protected override bool EqualsHandle(RG other)
        {
            return this._rg == other._rg;
        }

        protected override int GetHashCodeHandle()
        {
            var hashCode = -455586387;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(_rg);
            return hashCode;
        }

        public override string ToString()
            => _rg;
    }
}
