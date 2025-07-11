using School_Manager.Domain.Common;
using School_Manager.Domain.Entities.Catalog.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Manager.Domain.Entities.Catalog.Operation
{
    public class Driver : AuditableEntity<long>
    {
        /// <summary>
        /// شناسه کاربر
        /// </summary>
        public long UserRef { get; set; }
        /// <summary>
        /// شناسه بانک
        /// </summary>
        public int BankRef { get; set; }
        /// <summary>
        /// نام
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// نام خانوادگی
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// نام پدر
        /// </summary>
        public string FatherName { get; set; }
        /// <summary>
        /// شماره حساب
        /// </summary>
        public string BankAccountNumber {  get; set; }
        /// <summary>
        /// شماره گواهی
        /// </summary>
        public string CertificateId { get; set; }
        /// <summary>
        /// تحصیلات
        /// </summary>
        public string Education { get; set; }
        /// <summary>
        /// مشحصات
        /// </summary>
        public string Descriptions { get; set; }

        /// <summary>
        /// طول جرافیایی
        /// </summary>
        public double? Latitude { get; set; }
        /// <summary>
        /// عرض جغرافیایی
        /// </summary>
        public double? Longitude { get; set; }
        /// <summary>
        /// آدرس
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// تاریخ تولد
        /// </summary>
        public DateTime BirthDate { get; set; }
        /// <summary>
        /// کدملی
        /// </summary>
        public string NationCode { get; set; }
        /// <summary>
        /// صندلی خالی
        /// </summary>
        public int AvailableSeats { get; set; }
        /// <summary>
        /// امتیاز راننده
        /// </summary>
        public int Rate { get; set; }
        /// <summary>
        /// اخطار ها
        /// </summary>
        public int Warnning { get; set; }
        /// <summary>
        /// کد پرونده
        /// </summary>
        public string? Code { get; set; }
        /// <summary>
        /// مشخصات ماشین
        /// </summary>
        public virtual ICollection<Car> Cars { get; set; }
        /// <summary>
        /// لیست مسافر ها
        /// </summary>
        public virtual ICollection<DriverChild> Passanger { get; set; }
        /// <summary>
        /// قرارداد راننده
        /// </summary>
        public virtual ICollection<DriverContract> DriverContracts { get; set; }
        /// <summary>
        /// کاربر
        /// </summary>
        public virtual User UserNavigation { get; set; }
    }
}
