using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School_Manager.Core.Services.Interfaces;
using School_Manager.Core.ViewModels.FModels;
using School_Manger.Extension;
using School_Manger.Models;
using School_Manger.Models.PageView;
using SMS.Base;
using System.Threading.Tasks;

namespace School_Manger.Controllers.Admin
{
    [Area("Admin")][Authorize(Roles = "Admin")]
    public class DriverController : Controller
    {
        private readonly IDriverService _driverService;
        private readonly IChildService _childService;
        private readonly IUserService _userService;
        private readonly ISMSService _smsservice;
        private readonly IShiftService _shiftService;
        private readonly ISettingService _settingService;
        public DriverController(IDriverService driverService, IUserService userService, IChildService childService,ISMSService sMSService,IShiftService shiftService, ISettingService settingService)
        {
            _driverService = driverService;
            _userService = userService;
            _childService = childService;
            _smsservice = sMSService;
            _settingService = settingService;
            _shiftService = shiftService;
        }
        public async Task<IActionResult> Index()
        {
            var Drivers = await _driverService.GetDrivers();
            return View("Index",Drivers);
        }
        public IActionResult Details(long id,long ShiftId = 0)
        {
            var driver = _driverService.GetDriver(id);
            if (driver == null) return NotFound();
            var Sid = ShiftId == 0 ? _shiftService.GetDriverShifts(driver.Id).FirstOrDefault().Id : ShiftId;
            var passanger = _driverService.GetPassngers(Sid);
            AdminDriver admindashbord = new AdminDriver()
            {
                Driver = driver,
                Passanger = passanger
            };
            return View("Details", admindashbord);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DriverDto driver, long UserRef)
        {
            if (UserRef == null || UserRef == 0)
                return View("UserManagement/Drivers");
            _driverService.CreateDriver(new DriverCreateDto()
            {
                UserRef = UserRef,
                Name = driver.Name,
                LastName = driver.LastName,
                NationCode = driver.NationCode,
                Warnning = driver.Warnning,
                Rate = driver.Rate,
                BankRef = 0,//mustchange,
                FatherName = driver.FutherName,
                Education = driver.Education,
                Descriptions = driver.Descriptions,
                CertificateId = driver.CertificateId,
                BirthDate = driver.BirthDate,
                AvailableSeats = driver.AvailableSeats,
                Address = driver.Address,
                Latitude = 0,
                Longitude = 0,
                CarCreateDto = new CarCreateDto()
                {
                    Name = driver.Car.Name,
                    SeatNumber = driver.Car.SeatNumber,
                    carType = (int)driver.Car.carType,
                    ChrPlateNumber = driver.Car.PlateNumber,
                    ColorCode = 0,
                    FirstIntPlateNumber = int.Parse(driver.Car.PlateNumber.Substring(0, 2)),
                    SecondIntPlateNumber = int.Parse(driver.Car.PlateNumber.Substring(2, 3)),
                    ThirdIntPlateNumber = 47,
                    IsActive = true,
                }
            });
            _smsservice.Send(_userService.GetUserById(UserRef).Mobile, $"راننده گرامی شما در سیستم {_settingService.Get("CompanyName")} ثبت نام شدید لینک ورود شما" + Environment.NewLine + _settingService.Get("DriverSMSLink"));
            ControllerExtensions.ShowSuccess(this, "موفق", "راننده با موفقعیت اضافه شد");
            return View(driver);
        }
        public IActionResult DeletePassanger(long ChildId, long DriverId)
        {
            _childService.RemoveDriverFromChild(ChildId, DriverId);
            return Details(DriverId);
        }
        }
}
