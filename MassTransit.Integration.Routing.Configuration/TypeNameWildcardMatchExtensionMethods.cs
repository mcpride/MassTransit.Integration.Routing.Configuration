using System.Text.RegularExpressions;

namespace MassTransit.Integration.Routing.Configuration
{
    internal static class TypeNameWildcardMatchExtensionMethods
    {
        public static bool MatchesTypeNameFilter(this string text, string pattern)
        {
            pattern = pattern.Replace(".", @"\.").Replace("?", ".").Replace("*", ".*?");
            return new Regex(pattern, RegexOptions.IgnoreCase).IsMatch(text);
        }

        public static bool IsTypeNameFilter(this string value)
        {
            return (!string.IsNullOrWhiteSpace(value) && (value.Contains("*") || (value.Contains("?"))));
        }
    }
}
