using School_Manager.Domain.Common;
using School_Manager.Domain.Entities.Catalog.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Manager.Domain.Entities.Catalog.Operation
{
    public class Child : AuditableEntity<int>
    {
        /// <summary>
        /// شناسه راننده
        /// </summary>
        public int DriverRef { get; set; }
        /// <summary>
        /// شناسه والدین
        /// </summary>
         public int ParentRef { get; set; }
        /// <summary>
        /// شناسه مسیر مدرسه
        /// </summary>
        public int LocationPairRef { get; set; }
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
        public ClassNumber Class { get; set; }
        /// <summary>
        /// مسیر خانه تا مدرسه
        /// </summary>
        public LocationPair PathNavigation { get; set; }
        /// <summary>
        /// والدین
        /// </summary>
        public Parent ParentNavigation { get; set; }
        /// <summary>
        /// راننده
        /// </summary>
        public virtual ICollection<DriverChild> DriverChildNavigation { get; set; }
    }
}
