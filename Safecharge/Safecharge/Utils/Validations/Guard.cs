using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Safecharge.Utils
{
    public static class Guard
    {
        public static void RequiresNotNull(object parameter, string parameterName)
        {
            if (parameter == null)
            {
                throw new ArgumentNullException(parameterName);
            }
        }

        public static void RequiresLength(int? length, int lenghtRestrictionValue, string parameterName)
        {
            if (length.HasValue && length != lenghtRestrictionValue)
            {
                throw new ArgumentException($"{parameterName} should be exactly {lenghtRestrictionValue} characters long.");
            }

        }
        public static void RequiresMaxLength(int? length, int max, string parameterName)
        {
            if (length.HasValue && length > max)
            {
                throw new ArgumentException($"{parameterName} should be up to {max} characters long.");
            }
        }

        public static void RequiresLengthBetween(int? length, int min, int max, string parameterName)
        {
            if (length.HasValue && (length < min || length > max))
            {
                throw new ArgumentException($"The length of the {parameterName} should be between {min} and {max} symbols.");
            }
        }

        public static void RequiresPattern(string parameterValue, string pattern, string parameterName)
        {
            if (parameterValue != null && !Regex.IsMatch(parameterValue, pattern))
            {
                throw new ArgumentException($"{parameterName} should match the pattern {pattern}.");
            }
        }

        public static void RequiresBool(string parameterValue, string parameterName)
        {
            if (parameterValue != "0" && parameterValue != "1")
            {
                throw new ArgumentException($"{parameterName} should be either 0 or 1.");
            }
        }

        public static void RequiresDateInFormat(string parameterValue, string format, string parameterName)
        {
            if (parameterValue != null && !DateTime.TryParseExact(
                    parameterValue,
                    format,
                    System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.None,
                    out _))
            {
                throw new ArgumentException($"{parameterName} should be in the format {format}.");
            }
        }

        public static void RequiresAllowedValues<T>(T parameter, List<T> allowedValues, string parameterName)
            where T : class
        {
            if (parameter != null && !allowedValues.Contains(parameter))
            {
                throw new ArgumentException($"Allowed values for {parameterName} are: {string.Join(", ", allowedValues)}.");
            }
        }
    }
}