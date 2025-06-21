using Microsoft.AspNetCore.Mvc;
using School_Manager.Core.ViewModels.FModels;
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
                Driver = new DriverDto()
                {
                    Id = 1,
                    Name = "محسن",
                    LastName = "تست",
                    Car = new CarInfoDto()
                    {
                        Id = 1,
                        Name = "پراید",
                        PlateNumber = "142317",
                        SeatNumber = 4,
                        Color = "white"
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
                        FirstName = "رامین",
                        LastName = "یوسفی",
                        Class = "اول ابتدایی",
                        NationalCode = "0521234567",
                        DriverId = 1,
                        SchoolId = 1,
                        Path = new LocationPairModel()
                        {
                            ChildId = 1,
                            Id = 1,
                            Location1 = new LocationDataDto()
                            {
                                Address="مرکز شهر",
                                Latitude = 	34.0918,
                                Longitude = 49.6892
                            },
                            Location2 = new LocationDataDto()
                            {
                                Address="دانشگاه اراک",
                                Latitude = 34.0655,
                                Longitude = 49.7023
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
                    },new ChildInfo()
                    {
                        BirthDate = DateTime.Now,
                        Id = 2,
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
                            Location1 = new LocationDataDto()
                            {
                                Address="مرکز شهر",
                                Latitude =  34.1,
                                Longitude = 49.7
                            },
                            Location2 = new LocationDataDto()
                            {
                                Address="دانشگاه اراک",
                                Latitude = 34.0655,
                                Longitude = 49.7023
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
