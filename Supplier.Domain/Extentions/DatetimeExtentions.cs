using System;
using System.Collections.Generic;
using System.Text;

namespace SupplierReg.Domain
{
    public static class DatetimeExtentions
    {
        public static int Age(this DateTimeOffset birthdate, DateTimeOffset? today = null)
        {
            today = today ?? DateTimeOffset.Now;
            int age = today.Value.Year - birthdate.Year;
            if (today < birthdate.AddYears(age)) age--;
            return age;
        }
    }
}
