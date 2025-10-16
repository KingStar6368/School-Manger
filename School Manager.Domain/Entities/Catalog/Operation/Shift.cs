using School_Manager.Domain.Common;
using School_Manager.Domain.Entities.Catalog.Enums;

namespace School_Manager.Domain.Entities.Catalog.Operation
{
    public class Shift : AuditableEntity<long>
    {
        //مدرسه
        public long SchoolRef { get; set; }
        // نوع شیفت (صبح ) و غیره
        public ShiftType ShiftType { get; set; }
        //زمان شیفت
        public TimeOnly ShiftTime { get; set; }
        //ساعت شروع
        public TimeOnly Start {  get; set; }
        //ساعت پایان
        public TimeOnly End { get; set; }
        //مدرسه
        public virtual School SchoolNavigation { get; set; }
        public virtual ICollection<DriverShift> DriverShifts { get; set; }

    }
}
