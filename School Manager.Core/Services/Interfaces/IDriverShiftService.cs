using School_Manager.Core.ViewModels.FModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Manager.Core.Services.Interfaces
{
    public interface IDriverShiftService
    {
        public long CreateDriverShift(CreateDriverShiftDto driverShiftDto);
        public bool UpdateDriverShift(UpdateDriverShiftDto driverShiftDto);
        public bool DeleteDriverShift(int Id);
    }
}
