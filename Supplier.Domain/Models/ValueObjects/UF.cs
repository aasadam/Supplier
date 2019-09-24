using Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Globalization;
using Domain.Core;

namespace SupplierReg.Domain.Models.ValueObjects
{
    public class UF : ValueObject<UF>
    {
        public static readonly HashSet<KeyValuePair<string, string>> UFs = new HashSet<KeyValuePair<string, string>>()
        {
            new KeyValuePair<string, string>( "AC", "Acre" ),
            new KeyValuePair<string, string>( "AL", "Alagoas" ),
            new KeyValuePair<string, string>( "AP", "Amapá" ),
            new KeyValuePair<string, string>( "AM", "Amazonas" ),
            new KeyValuePair<string, string>( "BA", "Bahia" ),
            new KeyValuePair<string, string>( "CE", "Ceará" ),
            new KeyValuePair<string, string>( "DF", "Distrito Federal" ),
            new KeyValuePair<string, string>( "ES", "Espírito Santo" ),
            new KeyValuePair<string, string>( "GO", "Goiás" ),
            new KeyValuePair<string, string>( "MA", "Maranhão" ),
            new KeyValuePair<string, string>( "MT", "Mato Grosso" ),
            new KeyValuePair<string, string>( "MS", "Mato Grosso do Sul" ),
            new KeyValuePair<string, string>( "MG", "Minas Gerais" ),
            new KeyValuePair<string, string>( "PA", "Pará" ),
            new KeyValuePair<string, string>( "PB", "Paraíba" ),
            new KeyValuePair<string, string>( "PR", "Paraná" ),
            new KeyValuePair<string, string>( "PE", "Pernambuco" ),
            new KeyValuePair<string, string>( "PI", "Piauí" ),
            new KeyValuePair<string, string>( "RJ", "Rio de Janeiro" ),
            new KeyValuePair<string, string>( "RN", "Rio Grande do Norte" ),
            new KeyValuePair<string, string>( "RS", "Rio Grande do Sul" ),
            new KeyValuePair<string, string>( "RO", "Rondônia" ),
            new KeyValuePair<string, string>( "RR", "Roraima" ),
            new KeyValuePair<string, string>( "SC", "Santa Catarina" ),
            new KeyValuePair<string, string>( "SP", "São Paulo" ),
            new KeyValuePair<string, string>( "SE", "Sergipe" ),
            new KeyValuePair<string, string>( "TO", "Tocantins" )
        };

        private string _uf;

        private UF()
        {

        }

        public UF(string uf)
        {
            _uf = uf;
        }        

        public static bool IsUF(string uf)
            => UFs.Any(x => x.Key.EqualsIgnoreAccentsAndCase(uf) || x.Value.EqualsIgnoreAccentsAndCase(uf));

        public static implicit operator string(UF uf) => uf._uf;
        public static explicit operator UF(string uf) => new UF(uf);

        protected override bool EqualsHandle(UF other)
            => _uf.EqualsIgnoreAccentsAndCase(other._uf);

        protected override int GetHashCodeHandle()
        {
            var hashCode = -455586387;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(_uf);
            return hashCode;
        }

        public override string ToString()
            => _uf;
    }
}
