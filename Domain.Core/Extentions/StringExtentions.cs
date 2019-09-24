using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Domain.Core
{
    public static class StringExtentions
    {
        public static bool EqualsIgnoreAccentsAndCase(this string a, string toCompare)
            => string.Compare(a, toCompare, CultureInfo.InvariantCulture, CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreCase) == 0;
    }
}
