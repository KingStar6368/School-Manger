using Microsoft.AspNetCore.Mvc;
using School_Manager.Core.Services.Implemetations;
using School_Manager.Core.Services.Interfaces;
using School_Manager.Core.ViewModels.FModels;
using School_Manager.Domain.Entities.Catalog.Operation;
using School_Manger.Models;
using School_Manger.Models.PageView;
using SMS.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace School_Manger.Controllers
{
    public class DriverController : Controller
    {
        private readonly IDriverService driverService;
        private readonly IUserService userService;
        private readonly IChildService childService;
        private readonly IShiftService shiftService;
        private readonly IAppConfigService _appConfigService;
        public DriverController(IUserService userService, IChildService childService,IDriverService driverService, IShiftService shiftService,IAppConfigService appConfigService)
        {
            this.userService = userService;
            this.childService = childService;
            this.driverService = driverService;
            this.shiftService = shiftService;
            this._appConfigService = appConfigService;
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
        public async Task<IActionResult> Login(UserDTO user)
        {
            if(userService.CheckAuthorize(user.UserName, user.Password) != null)
                return await DriverPannel(user.UserName);
            else
                return View("Index");
        }
        [HttpGet]
        public async Task<IActionResult> DriverPannel(string UserName)
        {
            var driver = driverService.GetDriverNationCode(UserName);
            var shifts = shiftService.GetAllDriverShifts(driver.Id); // Use mapped ShiftDto list
            DriverDashbord dashbord = new DriverDashbord()
            {
                Driver = driver,
                Passanger = await shiftService.GetAllPassangerOfDriver(driver.Id),
                Shifts = shifts
            };
            return View("DriverPannel",dashbord);
        }

        [HttpGet]
        public IActionResult GetDriverShifts(long driverId)
        {
            var shifts = shiftService.GetDriverShifts(driverId);
            return Json(shifts);
        }

        [HttpGet]
        public IActionResult GetShiftPassengers(long driverId,long shiftId)
        {
            if (driverId == 0 || shiftId == 0)
                return Json(new List<School_Manager.Core.ViewModels.FModels.ChildInfo>());

            var driverShift = shiftService.GetDriverShift(shiftId, driverId);
            if (driverShift == null)
                return Json(new List<School_Manager.Core.ViewModels.FModels.ChildInfo>());

            // DriverService expects (DriverId, DriverShiftId)
            var drivershiftid = shiftService.GetDriverShift(shiftId, driverId).Id;
            var passengers = driverService.GetPassngers(driverShift.Id);
            return Json(passengers);
        }
        [HttpGet]
        public async Task<JsonResult> GetRoute(float fromLat, float fromLon, float toLat, float toLon)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    // Build the API URL with the provided coordinates
                    string apiUrl = _appConfigService.ApiUrl() + $"/Route?fromLat={fromLat}&fromLon={fromLon}&toLat={toLat}&toLon={toLon}";

                    // Make the GET request to the external API
                    HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

                    // Ensure the request was successful
                    response.EnsureSuccessStatusCode();

                    // Read the response content as string
                    string responseContent = await response.Content.ReadAsStringAsync();

                    // Return the JSON response from the external API
                    return new JsonResult(responseContent);
                }
            }
            catch (HttpRequestException ex)
            {
                // Handle HTTP request errors
                return new JsonResult(new { error = $"API request failed: {ex.Message}" })
                {
                    StatusCode = 500
                };
            }
            catch (Exception ex)
            {
                // Handle other errors
                return new JsonResult(new { error = $"An error occurred: {ex.Message}" })
                {
                    StatusCode = 500
                };
            }
        }
    }
}
