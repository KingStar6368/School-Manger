using System;
using System.Globalization;

namespace School_Manger.Extension
{
    public static class DateConverter
    {
        private static readonly PersianCalendar PersianCal = new PersianCalendar();

        /// <summary>
        /// Convert Gregorian DateTime to Persian Date string (yyyy/MM/dd)
        /// </summary>
        public static string ToPersianString(this DateTime miladi)
        {
            int year = PersianCal.GetYear(miladi);
            int month = PersianCal.GetMonth(miladi);
            int day = PersianCal.GetDayOfMonth(miladi);

            return $"{year:0000}/{month:00}/{day:00}";
        }

        public static DateTime ToPersain(this DateTime miladi)
        {
            int year = PersianCal.GetYear(miladi);
            int month = PersianCal.GetMonth(miladi);
            int day = PersianCal.GetDayOfMonth(miladi);

            return DateTime.Parse($"{year:0000}/{month:00}/{day:00}");
        }
        /// <summary>
        /// Convert Persian date string (yyyy/MM/dd) to Gregorian DateTime
        /// </summary>
        public static DateTime ToMiladi(this string persianDate)
        {
            if (string.IsNullOrWhiteSpace(persianDate))
                throw new ArgumentNullException(nameof(persianDate));

            var parts = persianDate.Split('/');
            if (parts.Length != 3)
                throw new FormatException("Invalid Persian date format. Expected yyyy/MM/dd");

            int year = int.Parse(parts[0]);
            int month = int.Parse(parts[1]);
            int day = int.Parse(parts[2]);

            return PersianCal.ToDateTime(year, month, day, 0, 0, 0, 0);
        }

        /// <summary>
        /// Takes either Persian (yyyy/MM/dd) or Gregorian (DateTime) and returns both formats.
        /// </summary>
        public static (string Persian, DateTime Miladi) ConvertAuto(string inputOrDate)
        {
            // Try parsing as Persian
            if (inputOrDate.Contains("/"))
            {
                var miladi = ToMiladi(inputOrDate);
                var persian = ToPersianString(miladi);
                return (persian, miladi);
            }

            throw new FormatException("Invalid input. Use Persian format yyyy/MM/dd.");
        }

        public static (string Persian, DateTime Miladi) ConvertAuto(DateTime miladi)
        {
            return (ToPersianString(miladi), miladi);
        }
    }
}
