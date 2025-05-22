using Microsoft.AspNetCore.Mvc;
using School_Manger.Models;

namespace School_Manger.Controllers
{
    public class DriverController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View("DriverPannel",new Driver()
            {
                Car = new CarInfo()
                {
                    AvailableSeats = 5,
                    Id = 1,
                    Name = "پراید",
                    PlateNumber = "142317"
                },
                Id=1,Name="محسن",
                LastName="تست",
                Passanger = new List<ChildInfo>()
                {
                    new ChildInfo()
                    {
                        HasPaid = true,
                        BirthDate = DateTime.Now,
                        Class = "دوم",
                        Id = 1,
                        FirstName = "تست",
                        LastName = "تست",
                        NationalCode = "52123",
                        Path = new LocationPairModel()
                        {
                            ChildId = 1,
                            Id = 1,
                            Location1 = new LocationData()
                            {
                                Address="شهرک حمید",
                                Latitude = 50,
                                Longitude = 60
                            },
                            Location2 = new LocationData()
                            {
                                Address="دانشگاه امیر کبیر",
                                Latitude = 90,
                                Longitude = 10
                            },
                            PickTime1 = DateTime.Now,
                            PickTime2 = DateTime.Now,
                        }
                    }
                }
            });
        }
    }
}
