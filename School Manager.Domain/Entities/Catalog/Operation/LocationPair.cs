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
        /// شناسه موقعیت خانه
        /// </summary>
        public int LocationRef1 { get; set; }
        /// <summary>
        /// شناسه موقعیت مدرسه
        /// </summary>
        public int LocationRef2 { get; set; }
        /// <summary>
        /// شناسه مسافر
        /// </summary>
        public int ChildRef { get; set; }
        /// <summary>
        /// مشحصات مسافر
        /// </summary>
        public Child ChildNavigation { get; set; }
        /// <summary>
        /// زمان رفت
        /// </summary>
        public DateTime PickTime1 { get; set; }
        /// <summary>
        /// زمان برگشت
        /// </summary>
        public DateTime PickTime2 { get; set; }
        /// <summary>
        /// موقعیت خانه
        /// </summary>
        public LocationData Location1 { get; set; }
        /// <summary>
        /// موقعیت مدرسه
        /// </summary>
        public LocationData Location2 { get; set; }
    }
}
