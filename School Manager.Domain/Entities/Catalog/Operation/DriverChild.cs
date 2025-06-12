using School_Manager.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Manager.Domain.Entities.Catalog.Operation
{
    public class DriverChild : AuditableEntity<long>
    {
        public int ChildRef { get; set; }
        public int DriverRef { get; set; }
        public virtual Child ChildNavigation { get; set; }
        public virtual Driver DriverNavigation { get; set; }
        public int Year { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsEnabled { get; set; }
    }
}
