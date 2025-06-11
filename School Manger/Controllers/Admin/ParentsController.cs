using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School_Manger.Extension;
using School_Manger.Models;
using School_Manger.Models.PageView;

namespace School_Manger.Controllers.Admin
{
    [Area("Admin")]
    public class ParentsController : Controller
    {
        List<Parent> TestModel = new List<Parent>()
        {
            new Parent()
            {
                Children = new List<ChildInfo>()
                {
                    new ChildInfo()
                    {
                            Id = 1,
                            FirstName = "حسین",
                            LastName = "بنیادی",
                            Class = "اول ابتدایی",
                            NationalCode = "0521234567",
                            DriverId = 1,
                            BirthDate = DateTime.Now.AddYears(-7).ToPersain(),
                            Path = new LocationPairModel()
                            {
                                ChildId = 1,
                                Id = 1,
                                Location1 = new LocationData()
                                {
                                    Address="مرکز شهر",
                                    Latitude =  34.0918,
                                    Longitude = 49.6892
                                },
                                Location2 = new LocationData()
                                {
                                    Address="دانشگاه اراک",
                                    Latitude = 34.0655,
                                    Longitude = 49.7023
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
                    new ChildInfo()
                        {
                            Id = 2,
                            FirstName = "محمد",
                            LastName = "بنیادی",
                            Class = "چهارم ابتدایی",
                            NationalCode = "0521234567",
                            BirthDate = DateTime.Now.AddYears(-11).ToPersain(),
                            Path = new LocationPairModel()
                            {
                                ChildId = 1,
                                Id = 1,
                                Location1 = new LocationData()
                                {
                                    Address="مرکز شهر",
                                    Latitude = 	34.0918,
                                    Longitude = 49.6892
                                },
                                Location2 = new LocationData()
                                {
                                    Address="دانشگاه اراک",
                                    Latitude = 34.0655,
                                    Longitude = 49.7023
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
                                    PaidPrice = 10,
                                    BillExpiredTime = DateTime.Now.AddMonths(-1),
                                    TotalPrice = 100
                                },
                                new Bill()
                                {
                                    Id = 3,
                                    Name = "آذر",
                                    ContractId = 1,
                                    PaidPrice = 0,
                                    BillExpiredTime = DateTime.Now.AddDays(1),
                                    TotalPrice = 100
                                },
                                new Bill()
                                {
                                    Id = 4,
                                    Name = "دی",
                                    ContractId = 1,
                                    PaidPrice = 0,
                                    BillExpiredTime = DateTime.Now.AddMonths(1),
                                    TotalPrice = 100
                                }
                            }
                        }
                },
                ParentFirstName = "رضا",
                ParentNationalCode = "0527654321",
                ParentLastName = "بنیادی"
            },
        };
        public IActionResult Index()
        {
            return View(TestModel);
        }
        public IActionResult Details(int id)
        {
            var Model = TestModel.FirstOrDefault(x => x.Id == id);
            AdminParent admindashbord = new AdminParent()
            {
                Parent = Model,
                Drivers = new List<Driver>()
                { new Driver
                    {
                        Id = 1,
                        Name = "رضا",
                        LastName = "محمدی",
                        Car = new CarInfo
                        {
                            Id = 1,
                            Name = "پراید",
                            PlateNumber = "12ب32647",
                            SeatNumber = 4,
                            Color = "white"
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
                    }
                },
            };
            return View(admindashbord);
        }
        [HttpPost]
        public IActionResult CreateBill(string Child)
        {
            return View(Child.DeCript<ChildInfo>());
        }
        //[HttpPost]
        //public IActionResult test(string Child)
        //{
        //    return StatusCode(200);
        //}
        //<iframe>
        //<form target = iframe name
    }
}
