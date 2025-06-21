using Microsoft.AspNetCore.Mvc;
using School_Manager.Core.ViewModels.FModels;
using School_Manger.Extension;
using School_Manger.Models;
using School_Manger.Models.PageView;

namespace School_Manger.Controllers.Admin
{
    [Area("Admin")]
    public class AssignD2SController : Controller
    {
        private AdminNONChildDriver Dashbord;
        public AssignD2SController() 
        {
            Dashbord = new AdminNONChildDriver()
            {
                NonDivers = new List<ChildInfo>()
                {
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
                                    PaidPrice = 10,
                                    BillExpiredTime = DateTime.Now.AddMonths(-1),
                                    TotalPrice = 100
                                },
                                new BillDto()
                                {
                                    Id = 3,
                                    Name = "آذر",
                                    ContractId = 1,
                                    PaidPrice = 0,
                                    BillExpiredTime = DateTime.Now.AddDays(1),
                                    TotalPrice = 100
                                },
                                new BillDto()
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
                AvailableDrivers = new List<DriverDto>() 
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
                            Color = "white"
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
                }
            };
        }
        public IActionResult Index()
        {
            return View(Dashbord);
        }
    }
}
