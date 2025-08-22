using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace TwinSharpControls
{

    internal static class HelperFunctions
    {

        internal static string FormatDouble(double value)
        {
            return value.ToString("0.0000", CultureInfo.InvariantCulture);
        }

        internal static string FormatDouble(double value, int decimalPlaces)
        {
            return value.ToString("0." + Zeroes(decimalPlaces), CultureInfo.InvariantCulture);
        }

        internal static string Zeroes(int num)
        {
            return new string('0', num);
        }
    }
}
