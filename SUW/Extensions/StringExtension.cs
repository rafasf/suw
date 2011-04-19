using System.Text.RegularExpressions;

namespace SUW.Extensions
{
    public static class StringExtension
    {
        public static string WithoutExtraSpaces(this string stringToClean)
        {
            if (string.IsNullOrEmpty(stringToClean))
                return string.Empty;

            return Regex.Replace(stringToClean.Trim(), @"\s+", " ");
        }

        public static string WithQuotes(this string @string)
        {
            return string.Format("\"{0}\"", @string);
        }
    }
}
