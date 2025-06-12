using School_Manager.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Manager.Domain.Entities.Catalog.Operation
{
    public class LocationPair : AuditableEntity<long>
    {
        /// <summary>
        /// شناسه مسافر
        /// </summary>
        public int ChildRef { get; set; }
        /// <summary>
        /// زمان رفت
        /// </summary>
        public DateTime PickTime1 { get; set; }
        /// <summary>
        /// زمان برگشت
        /// </summary>
        public DateTime PickTime2 { get; set; }
        /// <summary>
        /// موقعیت ها
        /// </summary>
        public virtual ICollection<LocationData> Locations { get; set; }
        /// <summary>
        /// مشحصات مسافر
        /// </summary>
        public virtual Child ChildNavigation { get; set; }
    }
}
