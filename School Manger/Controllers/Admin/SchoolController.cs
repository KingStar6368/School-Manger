using Microsoft.AspNetCore.Mvc;
using School_Manager.Core.ViewModels.FModels;
using School_Manger.Extension;
using School_Manger.Models.PageView;

namespace School_Manger.Controllers.Admin
{
    [Area("Admin")]
    public class SchoolController : Controller
    {
        private static List<SchoolDto> _schools = new()
        {
            new SchoolDto {
                Id = 1,
                Name = "دبیرستان نمونه دولتی البرز",
                ManagerName = "دکتر محمدی",
                Rate = 4,
                Address = new LocationDataDto {
                    Address = "تهران، خیابان انقلاب",
                    Latitude = 35.7025,
                    Longitude = 51.4356
                }
            },
            new SchoolDto {
                Id = 2,
                Name = "مدرسه غیرانتفاعی مهر",
                ManagerName = "خانم رضایی",
                Rate = 5,
                Address = new LocationDataDto {
                    Address = "تهران، خیابان ولیعصر",
                    Latitude = 35.7152,
                    Longitude = 51.4053
                }
            }
        };

        public IActionResult Index()
        {
            return View(_schools);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(SchoolDto model)
        {
            if (ModelState.IsValid)
            {
                model.Id = _schools.Max(s => s.Id) + 1;
                _schools.Add(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public IActionResult Details(long id)
        {
            var dashbord = new AdminSchool()
            {
                School = _schools.FirstOrDefault(x => x.Id == id),
                Drivers = new List<DriverDto>()
                {
                    new DriverDto {
                        Id = 1,
                        Name = "رضا",
                        LastName = "محمدی",
                        Car = new CarInfoDto
                        {
                            Id = 1,
                            Name = "پراید",
                            PlateNumber = "12ب32647",
                            SeatNumber = 4,
                            Color = "White",
                        },
                        NationCode = "05211312",
                        BankAccount = null,
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
                },
                Students = new List<ChildInfo>()
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
                                Location1 = new LocationDataDto()
                                {
                                    Address = "test",
                                    Latitude = 10,
                                    Longitude = 10,
                                    Name = "test",
                                },
                                Location2 = new LocationDataDto()
                                {
                                    Address = "test",
                                    Latitude = 10,
                                    Longitude = 10,
                                    Name = "test",
                                },
                                PickTime1 = DateTime.Now,
                                PickTime2 = DateTime.Now,
                            },
                            Bills = new List<BillDto>()
                            {
                                new BillDto()
                                {
                                    Id = 1,
                                    Name = "مهر",
                                    ContractId = 1,
                                    PaidPrice = 100,
                                    PaidTime = DateTime.Now,
                                    BillExpiredTime = DateTime.Now,
                                    TotalPrice = 100
                                },
                                new BillDto()
                                {
                                    Id = 2,
                                    Name = "آبان",
                                    ContractId = 1,
                                    PaidPrice = 100,
                                    PaidTime = DateTime.Now,
                                    BillExpiredTime = DateTime.Now.AddMonths(-1),
                                    TotalPrice = 100
                                },
                                new BillDto()
                                {
                                    Id = 3,
                                    Name = "آذر",
                                    ContractId = 1,
                                    PaidPrice = 100,
                                    PaidTime = DateTime.Now,
                                    BillExpiredTime = DateTime.Now.AddDays(1),
                                    TotalPrice = 100
                                },
                                new BillDto()
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
            return View(dashbord);
        }
    }
}
