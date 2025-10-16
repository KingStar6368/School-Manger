using School_Manager.Domain.Entities.Catalog.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Manager.Core.ViewModels.FModels
{
    public class DriverShiftDto
    {
        public long ShiftRef { get; set; }
        public long DriverRef { get; set; }
        //تعداد صندلی در هر شیفت راننده
        public int Seats { get; set; }
    }
    public class CreateDriverShiftDto
    {
        public long ShiftRef { get; set; }
        public long DriverRef { get; set; }
        //تعداد صندلی در هر شیفت راننده
        public int Seats { get; set; }
    }
    public class UpdateDriverShiftDto
    {
        public long Id { get; set; }
        public long ShiftRef { get; set; }
        public long DriverRef { get; set; }
        //تعداد صندلی در هر شیفت راننده
        public int Seats { get; set; }
    }
}
