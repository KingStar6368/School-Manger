using School_Manager.Domain.Entities.Catalog.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Manager.Core.ViewModels.FModels
{
    public class ShiftDto
    {
        public string Name { get; set; }
        //مدرسه
        public long SchoolRef { get; set; }
        // نوع شیفت (صبح ) و غیره
        public ShiftType ShiftType { get; set; }
        //زمان شیفت
        public TimeOnly ShiftTime { get; set; }
        //ساعت شروع
        public TimeOnly Start { get; set; }
        //ساعت پایان
        public TimeOnly End { get; set; }
    }
    public class CreateShiftDto
    {
        public string Name { get; set; }
        //مدرسه
        public long SchoolRef { get; set; }
        // نوع شیفت (صبح ) و غیره
        public ShiftType ShiftType { get; set; }
        //زمان شیفت
        public TimeOnly ShiftTime { get; set; }
        //ساعت شروع
        public TimeOnly Start { get; set; }
        //ساعت پایان
        public TimeOnly End { get; set; }
    }
    public class UpdateShiftDto
    {
        public long Id {  get; set; }
        public string Name { get; set; }
        //مدرسه
        public long SchoolRef { get; set; }
        // نوع شیفت (صبح ) و غیره
        public ShiftType ShiftType { get; set; }
        //زمان شیفت
        public TimeOnly ShiftTime { get; set; }
        //ساعت شروع
        public TimeOnly Start { get; set; }
        //ساعت پایان
        public TimeOnly End { get; set; }
    }
}
