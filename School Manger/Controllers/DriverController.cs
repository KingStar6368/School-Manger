using Microsoft.AspNetCore.Mvc;
using School_Manager.Core.Services.Interfaces;
using School_Manager.Core.ViewModels.FModels;
using School_Manger.Models;
using School_Manger.Models.PageView;

namespace School_Manger.Controllers
{
    public class DriverController : Controller
    {
        private readonly IDriverService driverService;
        private readonly IUserService userService;
        private readonly IChildService childService;
        public DriverController(IUserService userService, IChildService childService,IDriverService driverService)
        {
            this.userService = userService;
            this.childService = childService;
            this.driverService = driverService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View("Index");
        }
        [HttpPost]
        public IActionResult Login(UserDTO user)
        {
            if(userService.CheckAuthorize(user.UserName, user.Password) != null)
                return DriverPannel(user.UserName);
            else
                return View("Index");
        }
        [HttpGet]
        public IActionResult DriverPannel(string UserName)
        {
            var driver = driverService.GetDriverNationCode(UserName);
            DriverDashbord dashbord = new DriverDashbord()
            {
                Driver = driver,
                Passanger = driverService.GetPassngers(driver.Id)
            };
            return View("DriverPannel");
        }
    }
}
