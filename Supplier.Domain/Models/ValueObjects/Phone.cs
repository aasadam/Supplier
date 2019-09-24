using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SupplierReg.Domain.Models.ValueObjects
{
    public class Phone
    {
        private const char _splitChar = ' ';      
        

        private Phone()
        {

        }

        public Phone(bool isResidential, string stateCode, string phone, string countryCode = "55")
        {
            CountryCode = countryCode;
            StateCode = stateCode;
            PhoneNumber = phone;
            IsResidential = isResidential;
        }

        public Phone(bool isResidential, string concatenedPhone, char splitChar = _splitChar)
        {
            var phoneArray = concatenedPhone.Split(splitChar);

            CountryCode = phoneArray[0];
            StateCode = phoneArray[1];
            PhoneNumber = phoneArray[2];
            IsResidential = isResidential;
        }

        public string CountryCode { get; private set; }
        public string StateCode { get; private set; }
        public string PhoneNumber { get; private set; }
        public bool IsResidential { get; private set; }


        private static string ConcatPhone(string stateCode, string phone, string countryCode)
            => $"{countryCode}{_splitChar}{stateCode}{_splitChar}{phone}";


        public static bool IsPhone(string stateCode, string phone, string countryCode = "55")
            => !string.IsNullOrWhiteSpace(stateCode) && !string.IsNullOrWhiteSpace(phone) && !string.IsNullOrWhiteSpace(countryCode);

        public static implicit operator string(Phone phone) => ConcatPhone(phone.StateCode,phone.PhoneNumber,phone.CountryCode);
        //public static explicit operator Phone(string concatenedPhone) => new Phone(true, concatenedPhone);

        public override string ToString()
            => ConcatPhone(StateCode, PhoneNumber, CountryCode);
    }
}
