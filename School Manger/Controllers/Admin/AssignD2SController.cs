using Microsoft.AspNetCore.Mvc;
using School_Manager.Core.Services.Interfaces;
using School_Manager.Core.ViewModels.FModels;
using School_Manger.Extension;
using School_Manger.Models;
using School_Manger.Models.PageView;
using System.Threading.Tasks;

namespace School_Manger.Controllers.Admin
{
    [Area("Admin")]
    public class AssignD2SController : Controller
    {
        private readonly IChildService _childService;
        private readonly IDriverService _driverService;
        public AssignD2SController(IChildService childService,IDriverService driverService) 
        {
            _childService = childService;
            _driverService = driverService;
        }
        public async Task<IActionResult> Index()
        {
            var Childern = await _childService.GetChildWithoutDriver();
            var Drivers = await _childService.GetDriverFree();
            var Dashbord = new AdminNONChildDriver()
            {
                AvailableDrivers = Drivers,
                NonDivers = Childern,
            };
            return View("Index",Dashbord);
        }
        public async Task<IActionResult> AssignDriver(long DriverId, long StudentId)
        {
            bool result = _childService.SetDriver(StudentId, DriverId);
            if (result)
                ControllerExtensions.ShowSuccess(this, "موفق", "راننده اختصاص داده شد");
            else
                ControllerExtensions.ShowError(this,"خطا","مشکلی پیش آمده");
            return await Index();
        }
    }
}
