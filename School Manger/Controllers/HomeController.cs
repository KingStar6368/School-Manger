using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using School_Manger.Models;
using School_Manger.Models.ParentViews;

namespace School_Manger.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        #region Login&SignIn
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignIn()
        {
            return View("OTPConfirmation");
        }
        [HttpPost]
        public IActionResult VerifyOtp()
        {
            return View("UserInfo");
        }
        public IActionResult Login()
        {
            return View("Login");
        }
        #endregion
        #region AfterLogin
        [HttpPost]
        public IActionResult CompleteProfile()
        {
            return View("ParentMenu", new Parent()
            {
                Children = new List<ChildInfo>()
                {
                    new ChildInfo()
                    {
                        FirstName = "تست",
                        LastName = "test",
                        NationalCode = "0521744407",
                        BirthDate = DateTime.Now,
                        Id = 1,
                        Bills = new List<Bill>()
                        {
                            new Bill()
                            {
                                Id = 3,
                                ContractId = 1,
                                HasPaId = false,
                                PaidPrice = 10,
                                PaidTime = DateTime.Now,
                                TotalPrice = 100
                            },
                            new Bill()
                            {
                                Id = 3,
                                ContractId = 1,
                                HasPaId = false,
                                PaidPrice = 10,
                                PaidTime = DateTime.Now,
                                TotalPrice = 100
                            }
                        }
                    },
                    new ChildInfo()
                    {
                        FirstName = "تست",
                        LastName = "test",
                        NationalCode = "0521744407",
                        BirthDate = DateTime.Now,
                        Id = 2,
                        Bills = new List<Bill>()
                        {
                            new Bill()
                            {
                                Id = 3,
                                ContractId = 1,
                                HasPaId = true,
                                PaidPrice = 100,
                                PaidTime = DateTime.Now,
                                TotalPrice = 100
                            }
                        }
                    }
                },
                ParentFirstName = "اقای تست",
                ParentNationalCode = "0521744407",
                ParentLastName = "تست"

            });
        }
        [HttpPost]
        public IActionResult LocationSelector()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddChild(LocationPairModel model)
        {
            //Add Child And Show Menu Again
            return View("ParentMenu");
        }
        #endregion
        public IActionResult Bills()
        {
            List<Bill> bills = new List<Bill>()
            {
                new Bill()
                {
                    Id = 3,
                    ContractId = 1,
                    HasPaId = true,
                    PaidPrice = 100,
                    PaidTime = DateTime.Now,
                    TotalPrice = 100
                },
                new Bill()
                {
                    Id = 3,
                    ContractId = 1,
                    HasPaId = false,
                    PaidPrice = 10,
                    PaidTime = DateTime.Now,
                    TotalPrice = 100
                }
            };
            BillDashbord dashbord = new BillDashbord()
            {
                bills = bills,
            };
            return View(dashbord);
        }
        public IActionResult ShowBillPDF(BillDashbord index)
        {
            List<Bill> bills = new List<Bill>()
            {
                new Bill()
                {
                    Id = 3,
                    ContractId = 1,
                    HasPaId = true,
                    PaidPrice = 100,
                    PaidTime = DateTime.Now,
                    TotalPrice = 100
                },
                new Bill()
                {
                    Id = 3,
                    ContractId = 1,
                    HasPaId = false,
                    PaidPrice = 10,
                    PaidTime = DateTime.Now,
                    TotalPrice = 100
                }
            };

            Bill bill = bills.FirstOrDefault(x => x.Id == index.PDFInfoIndex);
            return View(bill);
        }
    }
}
