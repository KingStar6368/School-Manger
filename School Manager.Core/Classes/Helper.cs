using DNTPersianUtils.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
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

    public static class PersianDateHelper
    {
        public static IDictionary<int, string> PersianMonthNames { get; } = new Dictionary<int, string>
    {
        {
            1, "فروردین"
        },
        {
            2, "اردیبهشت"
        },
        {
            3, "خرداد"
        },
        {
            4, "تیر"
        },
        {
            5, "مرداد"
        },
        {
            6, "شهریور"
        },
        {
            7, "مهر"
        },
        {
            8, "آبان"
        },
        {
            9, "آذر"
        },
        {
            10, "دی"
        },
        {
            11, "بهمن"
        },
        {
            12, "اسفند"
        }
    };
        /// <summary>
        /// تبدیل یک تاریخ شمسی به میلادی
        /// </summary>
        public static DateTime ToMiladi(int year, int month, int day)
        {
            return $"{year}/{month:00}/{day:00}".ToGregorianDateTime() ?? DateTime.Now;
        }

        /// <summary>
        /// گرفتن سال شمسی از تاریخ میلادی
        /// </summary>
        public static int GetPersianYear(DateTime date)
        {
            return new PersianCalendar().GetYear(date);
        }

        /// <summary>
        /// گرفتن ماه شمسی از تاریخ میلادی
        /// </summary>
        public static int GetPersianMonth(DateTime date)
        {
            return new PersianCalendar().GetMonth(date);
        }

        /// <summary>
        /// برگرداندن تاریخ پرداخت با توجه به منطق قبض‌ها:
        /// - ماه بعد با DeadLine
        /// - اگر اسفند باشد، 25 اسفند
        /// </summary>
        public static DateTime GetBillEstimateTime(DateTime currentMonthDate, int deadlineDays)
        {
            int year = GetPersianYear(currentMonthDate);
            int month = GetPersianMonth(currentMonthDate);

            if (month == 12) // اسفند
            {
                return ToMiladi(year, 12, 25);
            }
            else
            {
                int nextMonth = month + 1;
                int nextYear = year;
                if (nextMonth > 12)
                {
                    nextMonth = 1;
                    nextYear++;
                }

                var baseDate = ToMiladi(nextYear, nextMonth, 1);
                return baseDate.AddDays(deadlineDays);
            }
        }
    }
}
