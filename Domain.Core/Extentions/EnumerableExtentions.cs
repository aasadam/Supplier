using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Domain.Core
{
    public static class EnumerableExtentions
    {
        public static string ConcatInOneString(this IEnumerable<string> toConcat)
        {
            return toConcat.Aggregate((i, j) => $"{i} \n {j}");
        }
    }
}
