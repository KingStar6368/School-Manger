using System.Diagnostics;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.AspNetCore.Mvc;
using School_Manger.Class;
using School_Manger.Models;
using School_Manger.Models.ParentViews;

namespace School_Manger.Controllers
{
    public class HomeController : Controller
    {
        //static data
        ParentDashbordView Static_Parent;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            Static_Parent = new ParentDashbordView()
            {
                Parent = new Parent()
                {
                    Children = new List<ChildInfo>()
                    {
                        new ChildInfo()
                        {
                            Id = 1,
                            FirstName = "حسین",
                            LastName = "بنیادی",
                            NationalCode = "0521234567",
                            BirthDate = DateTime.Now,
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
                                    BillExpiredTime = DateTime.Now.AddMonths(-1),
                                    TotalPrice = 100
                                },
                                new Bill()
                                {
                                    Id = 3,
                                    Name = "آذر",
                                    ContractId = 1,
                                    PaidPrice = 100,
                                    BillExpiredTime = DateTime.Now.AddDays(1),
                                    TotalPrice = 100
                                },
                                new Bill()
                                {
                                    Id = 4,
                                    Name = "دی",
                                    ContractId = 1,
                                    PaidPrice = 100,
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
                            NationalCode = "0521234567",
                            BirthDate = DateTime.Now,
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
        public IActionResult ParentMenu()
        {
            return View("ParentMenu",Static_Parent);
        }
        [HttpPost]
        public IActionResult CompleteProfile()
        {
            return ParentMenu();
        }
        [HttpPost]
        public IActionResult LocationSelector(ParentDashbordView view)
        {
            return View(view);
        }
        [HttpPost]
        public IActionResult AddChild(ParentDashbordView model)
        {
            model.SelectedChild.Bills = new List<Bill>();
            //this is for Test!!!!!!
            long Lastid = Static_Parent.Parent.Children.Select(x => x.Id).OrderBy(x => x).FirstOrDefault();
            model.SelectedChild.Id = Lastid++;
            //!!!!!!
            
            Static_Parent.Parent.Children.Add(model.SelectedChild);
            //Add Child And Show Menu Again
            return ParentMenu();
        }
        [HttpPost]
        public IActionResult RemoveChild(ParentDashbordView model)
        {
            Static_Parent.Parent.Children.Remove(Static_Parent.Parent.Children.FirstOrDefault(x => x.NationalCode == model.SelectedChild.NationalCode));
            return ParentMenu();
        }
        #endregion

        [HttpPost]
        public IActionResult Bills(ParentDashbordView model)
        {
            var selectedChild = Static_Parent.Parent.Children.FirstOrDefault(x => x.Id == model.SelectedChild.Id);
            #region Static Data
            //List<Bill> bills = new List<Bill>()
            //{
            //    new Bill()
            //    {
            //        Id = 3,
            //        Name = "مهر",
            //        ContractId = 1,
            //        PaidPrice = 100,
            //        PaidTime = DateTime.Now,
            //        BillExpiredTime = DateTime.Now,
            //        TotalPrice = 100
            //    },
            //    new Bill()
            //    {
            //        Id = 3,
            //        Name = "آبان",
            //        ContractId = 1,
            //        PaidPrice = 10,
            //        BillExpiredTime = DateTime.Now.AddMonths(-1),
            //        TotalPrice = 100
            //    },
            //    new Bill()
            //    {
            //        Id = 3,
            //        Name = "آذر",
            //        ContractId = 1,
            //        PaidPrice = 10,
            //        BillExpiredTime = DateTime.Now.AddDays(1),
            //        TotalPrice = 100
            //    },
            //    new Bill()
            //    {
            //        Id = 3,
            //        Name = "دی",
            //        ContractId = 1,
            //        PaidPrice = 10,
            //        BillExpiredTime = DateTime.Now.AddMonths(1),
            //        TotalPrice = 100
            //    }
            //}; 
            #endregion
            BillDashbord dashbord = new BillDashbord()
            {
                bills = selectedChild.Bills,
            };
            return View(dashbord);
        }
        [HttpPost]
        public IActionResult ShowBillPDF(BillDashbord index)
        {
            List<Bill> bills = new List<Bill>()
            {
                  new Bill()
                {
                    Id = 3,
                    Name = "مهر",
                    ContractId = 1,
                    PaidPrice = 100,
                    PaidTime = DateTime.Now,
                    BillExpiredTime = DateTime.Now,
                    TotalPrice = 100
                },
                new Bill()
                {
                    Id = 3,
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
                    PaidPrice = 10,
                    BillExpiredTime = DateTime.Now.AddDays(1),
                    TotalPrice = 100
                },
                new Bill()
                {
                    Id = 3,
                    Name = "دی",
                    ContractId = 1,
                    PaidPrice = 10,
                    BillExpiredTime = DateTime.Now.AddMonths(1),
                    TotalPrice = 100
                }
            };

            Bill bill = bills.FirstOrDefault(x => x.Id == index.PDFInfoIndex);
            PDFGenerator PDF = new PDFGenerator();
            PDF.TitlesPer = new List<Paragraph>()
            {
                new Paragraph("نمایش قبض")
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(24),
            };
            PDF.Tbl = new List<PDFTable>()
            {
                new PDFTable(2,60,HorizontalAlignment.CENTER)
                .AddRow("ID :",bill.Id)
                .AddRow("Title :",bill.Name)
                .AddRow("PaidPrice :",bill.PaidPrice)
                .AddRow("TotalPrice :",bill.TotalPrice)
                .AddRow("Is PaidFully :",bill.HasPaId)
                .AddRow("Status :",bill.HasPaId)
                .AddRow("Paid Time :",bill.PaidTime)
            };
            return File(PDF.Generate(), "application/pdf");
        }
    }
}
