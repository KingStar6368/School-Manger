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
        /// شناسه راننده
        /// </summary>
        public long DriverRef { get; set; }
        /// <summary>
        /// شناسه والدین
        /// </summary>
         public long ParentRef { get; set; }
        /// <summary>
        /// شناسه مسیر مدرسه
        /// </summary>
        public long LocationPairRef { get; set; }
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
        /// <summary>
        /// قرارداد
        /// </summary>
        public virtual ICollection<ServiceContract> ServiceContracts { get; set; }
    }
}
