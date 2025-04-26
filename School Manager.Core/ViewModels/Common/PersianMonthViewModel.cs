using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Manager.Core.ViewModels.Common
{
    public class PersianMonthViewModel
    {
        /// <summary>
        /// روز جاری
        /// </summary>
        public int Day { get; set; }
        /// <summary>
        /// ماه جاری
        /// </summary>
        public int Month { get; set; }
        /// <summary>
        /// سال شمسی
        /// </summary>
        public int Year { get; set; }
        /// <summary>
        ///     اولین روز ماه شمسی
        /// </summary>
        public DateTime StartDate { set; get; }

        /// <summary>
        ///     اولین روز ماه شمسی
        /// </summary>
        public DateOnly StartDateOnly { get; set; }

        /// <summary>
        ///     اولین روز ماه در هفته‌ای که واقع شده‌است، در تقویم شمسی هفتگی، چه شماره‌ی روزی را دارد؟
        ///     برای مثال سان‌دی معادل روز 2 هفته شمسی است
        /// </summary>
        public int StartDateDayOfWeek { set; get; }

        /// <summary>
        ///     آخرین روز ماه شمسی
        /// </summary>
        public DateTime EndDate { set; get; }

        /// <summary>
        ///     آخرین روز ماه شمسی
        /// </summary>
        public DateOnly EndDateOnly { get; set; }

        /// <summary>
        ///     آخرین روز ماه در هفته‌ای که واقع شده‌است، در تقویم شمسی هفتگی، چه شماره‌ی روزی را دارد؟
        ///     برای مثال سان‌دی معادل روز 2 هفته شمسی است
        /// </summary>
        public int EndDateDayOfWeek { set; get; }

        /// <summary>
        ///     شماره آخرین روز ماه شمسی را بر می‌گرداند که 29 یا 30 یا 31 است
        /// </summary>
        public int LastDayNumber { set; get; }
    }
}
