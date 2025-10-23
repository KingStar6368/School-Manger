using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School_Manager.Core.Services.Interfaces;
using School_Manager.Core.ViewModels.FModels;
using School_Manger.Extension;
using School_Manger.Models;
using School_Manger.Models.PageView;
using System.Threading.Tasks;

namespace School_Manger.Controllers.Admin
{
    [Area("Admin")][Authorize(Roles = "Admin")]
    public class AssignD2SController : Controller
    {
        private readonly IChildService _childService;
        private readonly IDriverService _driverService;
        private readonly IShiftService _shiftService;
        public AssignD2SController(IChildService childService,IDriverService driverService,IShiftService shiftService) 
        {
            _childService = childService;
            _driverService = driverService;
            _shiftService = shiftService;
        }
        public async Task<IActionResult> Index()
        {
            var Childern = await _childService.GetChildWithoutDriver();
            //todo: set shift id 
            var Drivers = await _childService.GetDriverFree(1);
            var Dashbord = new AdminNONChildDriver()
            {
                AvailableDrivers = Drivers,
                NonDivers = Childern,
            };
            return View("Index",Dashbord);
        }
        public async Task<IActionResult> ShiftIndex(long ShiftId)
        {
            var Children = await _shiftService.GetChildernOfShift(ShiftId);
            var DriverShifts = await _shiftService.GetDriversOfShift(ShiftId);
            ControllerExtensions.AddKey(this, "ShiftId", ShiftId);
            var Dashbrod = new AdminNONChildDriver()
            {
                AvailableDrivers = DriverShifts,
                NonDivers = Children,
            };
            return View("Index",Dashbrod);
        }
        public async Task<IActionResult> AssignDriver(long DriverId, long StudentId)
        {
            var ShiftId = ControllerExtensions.GetKey<long>(this, "ShiftId");
            var DriverShiftId = _shiftService.GetDriverShift(ShiftId, DriverId).Id;
            //todo: change driverId to DriverShiftId
            bool result = _childService.SetDriver(StudentId, DriverShiftId);
            if (result)
                ControllerExtensions.ShowSuccess(this, "موفق", "راننده اختصاص داده شد");
            else
                ControllerExtensions.ShowError(this,"خطا","مشکلی پیش آمده");
            return await Index();
        }
    }
}
