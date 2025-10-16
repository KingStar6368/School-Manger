using School_Manager.Core.ViewModels.FModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Manager.Core.Services.Interfaces
{
    public interface IShiftService
    {
        public long CreateShift(CreateShiftDto ShiftDto);
        public bool UpdateShift(UpdateShiftDto ShiftDto);
        public bool DeleteShift(int Id);
        ShiftDto GetShiftById(long id);
        List<ShiftDto> GetAllShifts();
    }
}
