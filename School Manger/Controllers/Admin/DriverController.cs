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
    public class DriverController : Controller
    {
        private readonly IDriverService _driverService;
        private readonly IUserService _userService;
        public DriverController(IDriverService driverService,IUserService userService)
        {
            _driverService = driverService;
            _userService = userService;
        }
        public async Task<IActionResult> Index()
        {
            var Drivers = await _driverService.GetDrivers();
            return View(Drivers);
        }
        public IActionResult Details(long id)
        {
            var driver = _driverService.GetDriver(id);
            if (driver == null) return NotFound();
            var passanger = _driverService.GetPassngers(id);
            AdminDriver admindashbord = new AdminDriver()
            {
                Driver = driver,
                Passanger = passanger
            };
            return View(admindashbord);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DriverDto driver,long UserRef)
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
            });
            ControllerExtensions.ShowSuccess(this, "موفق", "راننده با موفقعیت اضافه شد");
            return View(driver);
        }
    }
}
