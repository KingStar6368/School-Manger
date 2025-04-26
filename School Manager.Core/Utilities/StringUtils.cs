using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace School_Manager.Core.Utilities
{
    public static class StringUtils
    {
        //
        // Summary:
        //     Checks whether a string value is null or empty.
        //
        // Parameters:
        //   value:
        //     The string value to test.
        //
        // Returns:
        //     true if value is null or is a zero-length string (""); otherwise, false.
        public static bool IsEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }


        public static string TrimEnd(this string source, string value)
        {
            if (!source.EndsWith(value))
                return source;

            return source.Remove(source.LastIndexOf(value));
        }

        /// <summary>
        /// Summary:
        ///     Convert sring to boolean:
        /// Samples:
        ///     bool success = bool.TryParse("True",  out bool result); // success: True
        ///     bool success = bool.TryParse("False", out bool result); // success: True
        ///     bool success = bool.TryParse(null, out bool result); // success: False
        ///     bool success = bool.TryParse("thisIsNotABoolean", out bool result); // success: False
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool ToBoolean(this string value)
        {
            if (bool.TryParse(value, out bool result))
            {
                return result;
            }

            return false;
        }

        public static int ToInt(this string number, int defaultInt)
        {
            int resultNum = defaultInt;

            if (!string.IsNullOrEmpty(number))
                resultNum = Convert.ToInt32(number);

            return resultNum;
        }

        public static int ToInt(this string number)
        {
            if (string.IsNullOrEmpty(number))
                throw new InvalidOperationException("An empty value is not converted to a number");
            else if (number.IsNumeric())
                return Convert.ToInt32(number);
            else
                throw new InvalidOperationException($"This string '{number}' is not converted to a number");
        }

        /// <summary>
        ///     A string extension method that query if '@this' is numeric.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>true if numeric, false if not.</returns>
        public static bool IsNumeric(this string @this)
        {
            return !Regex.IsMatch(@this, "[^0-9]");
        }

    }
}
