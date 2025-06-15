using School_Manager.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public long LocationDataRef { get; set; }
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
        [Range(0, 5, ErrorMessage = "امتیاز باید بین ۰ تا ۵ باشد.")]
        public int Rate { get; set; } // 0-5
        /// <summary>
        /// آدرس
        /// </summary>
        public LocationData AddressNavigation { get; set; }
        public virtual ICollection<Child> Childs { get; set; }
    }
}
