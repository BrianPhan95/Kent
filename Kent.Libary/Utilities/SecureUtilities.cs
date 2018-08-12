using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Kent.Libary.Utilities
{
    public class SecureUtilities
    {
        private const string HTML_TAG_PATTERN = "<.*?>";
        public static string RemoveSqlInjection(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }

            return input.ToLower().Trim().Replace("'", "")
                     .Replace(";", "")
                     .Replace("--", "")
                     .Replace("/*", "")
                     .Replace("*/", "")
                     .Replace("xp_", "")
                     .Replace("sp_", "")
                     .Replace("[", "")
                     .Replace("]", "")
                     .Replace("%", "")
                     .Replace(".", "")
                     .Replace("_", "")
                     .Replace("*", "")
                     .Replace("union", "")
                     .Replace("admin", "")
                     .Replace("delete", "")
                     .Replace("drop", "")
                     .Replace("where", "")
                     .Replace("insert", "")
                     .Replace("select", "")
                     .Replace("1=0", "")
                     .Replace("1=1", "");
        }

        public static string RemoveXSS(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }

            input = input.ToLower().Trim();
            input = Regex.Replace(input, HTML_TAG_PATTERN, string.Empty);
            input = Regex.Replace(input, "javascript:", string.Empty);
            input = Regex.Replace(input, "vbscript:", string.Empty);
            input = Regex.Replace(input, @"alert.*\(?'", string.Empty);
            input = Regex.Replace(input, @"alert.*\(?""", string.Empty);

            return input;
        }
    }
}
