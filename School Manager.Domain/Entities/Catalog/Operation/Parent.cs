using School_Manager.Domain.Common;
using School_Manager.Domain.Entities.Catalog.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Manager.Domain.Entities.Catalog.Operation
{
    public class Parent : AuditableEntity<long>
    {
        /// <summary>
        /// شناسه کاربر
        /// </summary>
        public int UserRef { get; set; }
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
        /// آدرس
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// ؟فعال
        /// </summary>
        public bool Active { get; set; }
        /// <summary>
        /// لیست فرزندها
        /// </summary>
        public virtual ICollection<Child> Children { get; set; }
        /// <summary>
        /// کاربر
        /// </summary>
        public virtual User UserNavigation { get; set; }
    }
}
