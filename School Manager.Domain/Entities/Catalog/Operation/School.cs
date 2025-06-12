using School_Manager.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Manager.Domain.Entities.Catalog.Operation
{
    public class School : AuditableEntity<long>
    {
        /// <summary>
        /// شناسه آدرس
        /// </summary>
        public int LocationDataRef { get; set; }
        /// <summary>
        /// نام مدسه
        /// </summary>
        public string Name { get; set; } 
        /// <summary>
        /// نام مدیر
        /// </summary>
        public string ManagerName { get; set; } 
        /// <summary>
        /// امتیاز
        /// </summary>
        public int Rate { get; set; } // 0-5
        /// <summary>
        /// آدرس
        /// </summary>
        public LocationData AddressNavigation { get; set; }
    }
}
