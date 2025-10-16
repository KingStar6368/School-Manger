using School_Manager.Domain.Common;
using School_Manager.Domain.Entities.Catalog.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Manager.Domain.Entities.Catalog.Operation
{
    public class Child : AuditableEntity<long>
    {
        /// <summary>
        /// شناسه والدین
        /// </summary>
         public long ParentRef { get; set; }
        /// <summary>
        /// شناسه مدرسه
        /// </summary>
        public long? SchoolRef { get; set; }
        /// <summary>
        /// شیفت مدرسه
        /// </summary>
        public long? ShiftId {  get; set; }
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
        /// توضیحات
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// تاریخ تولد
        /// </summary>
        public DateTime BirthDate { get; set; }
        /// <summary>
        /// کلاس تحصیلی
        /// </summary>
        public ClassNumber Class { get; set; }
        /// <summary>
        /// مسیر خانه تا مدرسه
        /// </summary>
        public virtual ICollection<LocationPair> LocationPairs { get; set; }
        /// <summary>
        /// والدین
        /// </summary>
        public Parent ParentNavigation { get; set; }
        /// <summary>
        /// راننده
        /// </summary>
        public virtual ICollection<DriverChild> DriverChilds { get; set; }
        /// <summary>
        /// قرارداد
        /// </summary>
        public virtual ICollection<ServiceContract> ServiceContracts { get; set; }
        public virtual School SchoolNavigation { get; set; }
        public virtual Shift ShiftNavigation { get; set; }
    }
}
