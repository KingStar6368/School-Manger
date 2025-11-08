using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School_Manager.Core.Services.Interfaces;
using School_Manager.Core.ViewModels.FModels;
using School_Manger.Extension;
using School_Manger.Models.PageView;
using System.Threading.Tasks;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

namespace School_Manger.Controllers.Admin
{
    [Area("Admin")][Authorize(Roles = "Admin")]
    public class SchoolController : Controller
    {
        private readonly ISchoolService _schoolService;
        private readonly IChildService _childService;
        private readonly IDriverService _driverService;
        private readonly IShiftService _shiftService;
        public SchoolController(ISchoolService schoolService, IChildService childService, IDriverService driverService, IShiftService shiftService)
        {
            _schoolService = schoolService;
            _childService = childService;
            _driverService = driverService;
            _shiftService = shiftService;
        }

        public async Task<IActionResult> Index()
        {
            var Schools = await _schoolService.GetSchools();
            return View("Index", Schools);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(SchoolDto model)
        {
            _schoolService.CreateSchool(new SchoolCreateDto()
            {
                Name = model.Name,
                ManagerName = model.ManagerName,
                Rate = 0,
                Address = model.Address.Address,
                Latitude = model.Address.Latitude,
                Longitude = model.Address.Longitude
            });
            ControllerExtensions.ShowSuccess(this, "موفق", "مدرسه با موفقعیت اضافه شد");
            return View(model);
        }
        public async Task<IActionResult> Details(long id)
        {
            var School = _schoolService.GetSchool(id);
            if (School == null)
                return await Index();
            var Drivers = await _schoolService.GetDrivers(id);
            var Childern = await _schoolService.GetChildren(id);
            AdminSchool dashbord = new AdminSchool()
            {
                School = School,
                Drivers = Drivers,
                Students = Childern
            };
            return View("Details", dashbord);
        }
        public async Task<IActionResult> DeleteSchool(long id)
        {
            try
            {
                if (_schoolService.DeleteSchool(id))
                    ControllerExtensions.ShowSuccess(this, "موفق", "مدرسه حذف شد");
                else
                    ControllerExtensions.ShowError(this, "خطا", "مشکلی در حذف پیش آمده");
            }
            catch (Exception ex)
            {
                ControllerExtensions.ShowError(this, "خطا", ex.Message);
            }
            return await Index();
        }
        [HttpGet]
        public IActionResult EditSchool(long SchoolId)
        {
            var school = _schoolService.GetSchool(SchoolId);
            return View(new SchoolUpdateDto()
            {
                Id = school.Id,
                Address = school.Address.Address,
                Latitude = school.Address.Latitude,
                Longitude = school.Address.Longitude,
                ManagerName = school.ManagerName,
                Name = school.Name,
                Rate = school.Rate
            });
        }
        [HttpPost]
        public IActionResult EditSchool(SchoolUpdateDto model)
        {
            if (ModelState.IsValid)
            {
                if (_schoolService.UpdateSchool(model))
                {
                    ControllerExtensions.ShowSuccess(this, "موفق", "مدرسه با موفقیت ویرایش شد");
                    return RedirectToAction("Details", new { id = model.Id });
                }
                else
                {
                    ControllerExtensions.ShowError(this, "خطا", "مشکلی در ویرایش پیش آمده");
                }
            }
            return View("Index");
        }
        public async Task<IActionResult> Shift(long id)
        {
            var School = _schoolService.GetSchool(id);
            if (School == null)
                return await Index();
            var Drivers = await _driverService.GetDrivers(id);
            var Childern = await _schoolService.GetChildren(id);
            AdminSchool dashbord = new AdminSchool()
            {
                School = School,
                Drivers = Drivers,
                Students = Childern
            };
            return View("Shift",dashbord);
        }

        [HttpPost]
        public IActionResult CreateShift(CreateShiftDto model)
        {
            if (ModelState.IsValid)
            {
                _shiftService.CreateShift(model);
                ControllerExtensions.ShowSuccess(this, "موفق", "شیفت با موفقیت اضافه شد");
            }
            else
            {
                ControllerExtensions.ShowError(this, "خطا", "اطلاعات وارد شده صحیح نیست");
            }
            return RedirectToAction("Shift", new { id = model.SchoolRef });
        }

        [HttpGet]
        public async Task<IActionResult> EditShift(long id)
        {
            var shift = _shiftService.GetShiftById(id);
            ViewBag.EditingShift = shift;
            var schoolId = shift.SchoolRef;
            var School = _schoolService.GetSchool(schoolId);
            var Drivers = await _driverService.GetDrivers(schoolId);
            var Childern = await _schoolService.GetChildren(schoolId);
            AdminSchool dashbord = new AdminSchool()
            {
                School = School,
                Drivers = Drivers,
                Students = Childern
            };
            return View("Shift", dashbord);
        }

        [HttpPost]
        public IActionResult UpdateShift(UpdateShiftDto model)
        {
            if (ModelState.IsValid)
            {
                _shiftService.UpdateShift(model);
                ControllerExtensions.ShowSuccess(this, "موفق", "شیفت با موفقیت ویرایش شد");
            }
            else
            {
                ControllerExtensions.ShowError(this, "خطا", "اطلاعات وارد شده صحیح نیست");
            }
            return RedirectToAction("Shift", new { id = model.SchoolRef });
        }

        [HttpPost]
        public IActionResult DeleteShift(long id)
        {
            var shift = _shiftService.GetShiftById(id);
            var schoolId = shift?.SchoolRef ?? 0;
            _shiftService.DeleteShift((int)id);
            ControllerExtensions.ShowSuccess(this, "موفق", "شیفت حذف شد");
            return RedirectToAction("Shift", new { id = schoolId });
        }
    }
}
