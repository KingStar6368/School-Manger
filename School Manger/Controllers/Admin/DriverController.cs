using Microsoft.AspNetCore.Mvc;
using School_Manger.Models;

namespace School_Manger.Controllers.Admin
{
    [Area("Admin")]
    public class DriverController : Controller
    {
        // Mock data - no database needed
        private List<Driver> _drivers = new List<Driver>
        {
        new Driver { Id = 1, Name = "رضا", LastName = "محمدی",
                    Car = new CarInfo { Name = "پراید", PlateNumber = "12ب12345", AvailableSeats = 3 } },
        new Driver { Id = 2, Name = "علی", LastName = "کریمی",
                    Car = new CarInfo { Name = "پژو", PlateNumber = "34الف123", AvailableSeats = 4 } }
        };

        public IActionResult Index()
        {
            return View(_drivers);
        }
        public IActionResult Details(int id)
        {
            var driver = _drivers.FirstOrDefault(d => d.Id == id);
            if (driver == null) return NotFound();
            return View(driver);
        }
    }
}
