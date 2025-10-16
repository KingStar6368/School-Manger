using School_Manager.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Manager.Domain.Entities.Catalog.Operation
{
    public class DriverShift : AuditableEntity<long>
    {
        public long ShiftRef {  get; set; }
        public long DriverRef { get; set; }
        //تعداد صندلی در هر شیفت راننده
        public int Seats { get; set; }
        public virtual Shift ShiftNavigation { get; set; }
        public virtual Driver DriverNavigation { get; set; }
        public virtual ICollection<DriverChild> Passenger { get; set; }
    }
}
