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
        /// نام مدرسه
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
        public string Address { get; set; }
        /// <summary>
        /// طول جغرافیایی
        /// </summary>
        public double Latitude { get; set; }
        /// <summary>
        /// عرض جغرافیایی
        /// </summary>
        public double Longitude { get; set; }
        public virtual ICollection<Child> Childs { get; set; }
        public virtual ICollection<Shift> Shifts { get; set; }
    }
}
