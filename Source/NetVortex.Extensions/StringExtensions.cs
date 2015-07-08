using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetVortex.Extensions
{
    public static class StringExtensions
    {
        public static string ExecuteIfNotNullOrWhitespace(this string s, Action action)
        {
            if (!string.IsNullOrWhiteSpace(s) && s.ToUpper() != "NULL")
                action();

            return s;
        }
    }
}
