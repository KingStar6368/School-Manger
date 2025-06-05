using Microsoft.AspNetCore.Mvc;
using School_Manger.Models;
using School_Manger.Models.PageView;

namespace School_Manger.Controllers
{
    public class DriverController : Controller
    {
        private DriverDashbord Dashbord;
        public DriverController()
        {
            Dashbord = new DriverDashbord()
            {
                Driver = new Driver()
                {
                    Id = 1,
                    Name = "محسن",
                    LastName = "تست",
                    Car = new CarInfo()
                    {
                        AvailableSeats = 5,
                        Id = 1,
                        Name = "پراید",
                        PlateNumber = "142317"
                    },
                    Passanger = new List<long>()
                    {
                        1
                    }
                },
                Passanger = new List<ChildInfo>()
                {
                    new ChildInfo()
                    {
                        BirthDate = DateTime.Now,
                        Id = 1,
                        FirstName = "حسین",
                        LastName = "بنیادی",
                        Class = "اول ابتدایی",
                        NationalCode = "0521234567",
                        DriverId = 1,
                        SchoolId = 1,
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
                        },
                        Bills = new List<Bill>()
                        {
                            new Bill()
                            {
                                Id = 1,
                                Name = "مهر",
                                ContractId = 1,
                                PaidPrice = 100,
                                PaidTime = DateTime.Now,
                                BillExpiredTime = DateTime.Now,
                                TotalPrice = 100
                            },
                            new Bill()
                            {
                                Id = 2,
                                Name = "آبان",
                                ContractId = 1,
                                PaidPrice = 100,
                                PaidTime = DateTime.Now,
                                BillExpiredTime = DateTime.Now.AddMonths(-1),
                                TotalPrice = 100
                            },
                            new Bill()
                            {
                                Id = 3,
                                Name = "آذر",
                                ContractId = 1,
                                PaidPrice = 100,
                                PaidTime = DateTime.Now,
                                BillExpiredTime = DateTime.Now.AddDays(1),
                                TotalPrice = 100
                            },
                            new Bill()
                            {
                                Id = 4,
                                Name = "دی",
                                ContractId = 1,
                                PaidPrice = 100,
                                PaidTime = DateTime.Now,
                                BillExpiredTime = DateTime.Now.AddMonths(1),
                                TotalPrice = 100
                            }
                        }
                    }
                }
            };
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View("DriverPannel",Dashbord);
        }
    }
}
