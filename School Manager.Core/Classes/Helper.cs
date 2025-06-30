using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Manager.Core.Classes
{
    public static class StaticString
    {
        public static string LookUpBankType = "BankAccount";
        public static string LookUpColorType = "Color";
        public static string ParentList = "Parents";
    }
    public static class GeoUtils
    {
        private const double EarthRadiusKm = 6371.0;

        public static double Haversine(double lat1, double lon1, double lat2, double lon2)
        {

            double dLat = ToRadians(lat2 - lat1);
            double dLon = ToRadians(lon2 - lon1);

            lat1 = ToRadians(lat1);
            lat2 = ToRadians(lat2);

            double a = Math.Pow(Math.Sin(dLat / 2), 2) +
                       Math.Pow(Math.Sin(dLon / 2), 2) * Math.Cos(lat1) * Math.Cos(lat2);
            double c = 2 * Math.Asin(Math.Sqrt(a));

            return EarthRadiusKm * c * 1000; // بر حسب متر
        }

        private static double ToRadians(double angle)
        {
            return angle * Math.PI / 180;
        }
    }
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            var displayAttribute = enumValue
                .GetType()
                .GetMember(enumValue.ToString())
                .First()
                .GetCustomAttributes(false)
                .OfType<DisplayAttribute>()
                .FirstOrDefault();

            return displayAttribute?.Name ?? enumValue.ToString();
        }
    }
}
