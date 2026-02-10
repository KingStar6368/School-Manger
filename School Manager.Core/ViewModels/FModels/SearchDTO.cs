using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Manager.Core.ViewModels.FModels
{
    public class SearchDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? NationalCode { get; set; }
        public string? Mobile { get; set; }
        // بخش قبض ها
        /// <summary>
        /// تاریخ شروع اجباری نیست
        /// </summary>
        public DateTime? StartDate { get; set; }
        /// <summary>
        /// تاریخ پایان اجباری نیست
        /// </summary>
        public DateTime? EndDate { get; set; }
        /// <summary>
        /// پرداختی ها
        /// </summary>
        public bool? HasPaid { get; set; }
        /// <summary>
        /// شماره ماه مثال 9 آذر ماه
        /// </summary>
        public int? MonthInt { get; set; }
        public string? BillName { get; set; }

        // Pagination
        public int Page { get; set; } = 1;   // شماره صفحه
        public int PageSize { get; set; } = 20;

    }
}
