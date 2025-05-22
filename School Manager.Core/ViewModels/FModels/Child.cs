using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Manager.Core.ViewModels.FModels
{
    /// <summary>
    /// کلاس فرزند
    /// </summary>
    public class ChildInfo
    {
        /// <summary>
        /// کد
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// نام
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// نام خانوادگی
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// کدملی
        /// </summary>
        public string NationalCode { get; set; }
        /// <summary>
        /// تاریخ تولد
        /// </summary>
        public DateTime BirthDate { get; set; }
        /// <summary>
        /// کلاس تحصیلی
        /// </summary>
        public string Class { get; set; }
        /// <summary>
        /// کامل پرداخت شده ؟
        /// </summary>
        public bool HasPaid { get; set; }
        /// <summary>
        /// مسیر خانه تا مدرسه
        /// </summary>
        public LocationPairModel Path { get; set; }
    }
}
