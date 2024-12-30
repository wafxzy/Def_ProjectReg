using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonReg.Common.Utils
{
    public static class StringUtils
    {
        public static string Truncate(this string value, int maxLength)
        {
            if (string.IsNullOrWhiteSpace(value)) return string.Empty;

            return value.Length <= maxLength ? value : value[..maxLength];

        }
    }
}
