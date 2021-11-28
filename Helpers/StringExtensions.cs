using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace dotnet.Helpers
{
    public static class StringExtensions
    {
        public static string CamelCase(this String s)
        {
            return String.Join(".", s.Split(".").Select(s => s._CamelCase()));
        }


        private static string _CamelCase(this String s)
        {
            var x = s.Replace("_", "");
            if (x.Length == 0) return "null";
            x = Regex.Replace(x, "([A-Z])([A-Z]+)($|[A-Z])",
                m => m.Groups[1].Value + m.Groups[2].Value.ToLower() + m.Groups[3].Value);
            return char.ToLower(x[0]) + x.Substring(1);
        }
    }
}






