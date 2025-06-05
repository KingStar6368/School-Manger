using Microsoft.AspNetCore.Mvc;
using School_Manger.Extension;
using School_Manger.Models;
using School_Manger.Models.PageView;

namespace School_Manger.Controllers.Admin
{
    [Area("Admin")]
    public class DriverController : Controller
    {
        // Mock data - no database needed
        private List<Driver> _drivers = new List<Driver>
        {
            new Driver {
                Id = 1,
                Name = "رضا",
                LastName = "محمدی",
                Car = new CarInfo
                {
                    Name = "پراید",
                    PlateNumber = "12ب32647",
                    AvailableSeats = 3
                },
                NationCode = "05211312",
                BankAccount = "12312",
                BankNumber = "1231321",
                Address = "اراک",
                BirthDate = DateTime.Parse("1375/04/01"),
                CertificateId = "123",
                Descriptions = "",
                Education = "دیپلم",
                FutherName = "احمد",
                Rate = 5,
                Warnning = 0,
                Passanger = new List<long>()
                {
                    1
                }
            },
        };

        public IActionResult Index()
        {
            return View(_drivers);
        }
        public IActionResult Details(int id)
        {
            var driver = _drivers.FirstOrDefault(d => d.Id == id);
            if (driver == null) return NotFound();
            AdminDriver admindashbord = new AdminDriver()
            {
                Driver = driver,
                Passanger = new List<ChildInfo>()
                {
                    new ChildInfo()
                        {
                            Id = 1,
                            FirstName = "حسین",
                            LastName = "بنیادی",
                            Class = "اول ابتدایی",
                            NationalCode = "0521234567",
                            BirthDate = DateTime.Now.AddYears(-7).ToPersain(),
                            Path = new LocationPairModel()
                            {
                                ChildId = 1,
                                Id = 1,
                                Location1 = new LocationData()
                                {
                                    Address = "test",
                                    Latitude = 10,
                                    Longitude = 10,
                                    Name = "test",
                                },
                                Location2 = new LocationData()
                                {
                                    Address = "test",
                                    Latitude = 10,
                                    Longitude = 10,
                                    Name = "test",
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
                        },
                }
            };
            return View(admindashbord);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Driver driver)
        {
            if (ModelState.IsValid)
            {
                // Save to database or list
                driver.Id = _drivers.Max(d => d.Id) + 1;
                _drivers.Add(driver);
                return RedirectToAction(nameof(Index));
            }
            return View(driver);
        }
    }
}
