using System.Diagnostics;
using DNTPersianUtils.Core;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.AspNetCore.Mvc;
using PersianTextShaper;
using School_Manager.Core.Services.Interfaces;
using School_Manager.Core.ViewModels.FModels;
using School_Manager.Domain.Entities.Catalog.Enums;
using School_Manger.Class;
using School_Manger.Extension;
using School_Manger.Models;
using School_Manger.Models.PageView;

namespace School_Manger.Controllers
{
    public class HomeController : Controller
    {
        //static data
        ParentDashbordView Static_Parent;
        private readonly IParentService _PService;
        private readonly IChildService _CService;
        private readonly IUserService _UserService;
        public HomeController(IParentService PService,IChildService CService,IUserService UService)
        {
            _PService = PService;
            _CService = CService;
            _UserService = UService;
            Static_Parent = new ParentDashbordView()
            {
                Parent = new ParentDto()
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
                            BirthDate = DateTime.Now.AddYears(-7).ToPersain(),
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
                        new ChildInfo()
                        {
                            Id = 2,
                            FirstName = "محمد",
                            LastName = "بنیادی",
                            Class = "چهارم ابتدایی",
                            NationalCode = "0521234567",
                            BirthDate = DateTime.Now.AddYears(-11).ToPersain(),
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
        public IActionResult SignIn(string PhoneNumber)
        {
            TempData["PhoneNumber"] = PhoneNumber;
            //Otp
            return View("OTPConfirmation");
        }
        [HttpPost]
        public IActionResult VerifyOtp()
        {
            //OtpCode Confirm
            return View("UserInfo");
        }
        public IActionResult Login()
        {
            //Todo Verfiy User 
            return View("Login");
        }
        [HttpPost]
        public IActionResult CompleteProfile(string nationalCode, string firstName, string lastName, string password)
        {
            //TODO Create User & Parent
            //LoginPageData pageData = Data.DeCript<LoginPageData>();
            LoginUser user = new LoginUser()
            {
                UserName = nationalCode,
                PhoneNumber = TempData["PhoneNumber"].ToString(),
                Password = password,
                Type = UserType.Parent,
            };
            ParentDto parent = new ParentDto()
            {
                Active = true,
                ParentFirstName = firstName,
                ParentLastName = lastName,
                ParentNationalCode = nationalCode,
            };
            long UseRref = _UserService.CreateUser(new School_Manager.Core.ViewModels.FModels.UserCreateDTO()
            {
                FirstName = parent.ParentFirstName,
                IsActive = true,
                LastName = parent.ParentLastName,
                Mobile = user.PhoneNumber,
                PasswordHash = user.Password,
                UserName = user.UserName
            });
            long ParentRef = _PService.CreateParent(new School_Manager.Core.ViewModels.FModels.ParentCreateDto()
            {
                FirstName = parent.ParentFirstName,
                LastName = parent.ParentLastName,
                NationalCode = parent.ParentNationalCode,
                UserRef = UseRref,
                Active = true,
                Address = "",

            });
            TempData["Uref"] = UseRref;
            TempData["Pref"] = ParentRef;
            //Parent
            return ParentMenu();
        }
        #endregion

        #region AfterLogin
        public IActionResult ParentMenu()
        {
            long Uref = long.Parse(TempData["Uref"].ToString());
            long Pref = long.Parse(TempData["Pref"].ToString());
            ParentDto parent = _PService.GetParent(Pref);
            return View("ParentMenu",Static_Parent);
        }
        [HttpPost]
        public IActionResult LocationSelector(ParentDashbordView view)
        {
            return View(view);
        }
        [HttpPost]
        public IActionResult AddChild(ParentDashbordView model)
        {
            model.SelectedChild.Bills = new List<BillDto>();
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
        public IActionResult Bills(int Id)
        {
            var selectedChild = Static_Parent.Parent.Children.FirstOrDefault(x => x.Id == Id);
            #region Static Data
            //List<BillDto> bills = new List<BillDto>()
            //{
            //    new BillDto()
            //    {
            //        Id = 3,
            //        Name = "مهر",
            //        ContractId = 1,
            //        PaidPrice = 100,
            //        PaidTime = DateTime.Now,
            //        BillExpiredTime = DateTime.Now,
            //        TotalPrice = 100
            //    },
            //    new BillDto()
            //    {
            //        Id = 3,
            //        Name = "آبان",
            //        ContractId = 1,
            //        PaidPrice = 10,
            //        BillExpiredTime = DateTime.Now.AddMonths(-1),
            //        TotalPrice = 100
            //    },
            //    new BillDto()
            //    {
            //        Id = 3,
            //        Name = "آذر",
            //        ContractId = 1,
            //        PaidPrice = 10,
            //        BillExpiredTime = DateTime.Now.AddDays(1),
            //        TotalPrice = 100
            //    },
            //    new BillDto()
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
            List<BillDto> bills = new List<BillDto>()
            {
                  new BillDto()
                {
                    Id = 3,
                    Name = "مهر",
                    ContractId = 1,
                    PaidPrice = 100,
                    PaidTime = DateTime.Now,
                    BillExpiredTime = DateTime.Now,
                    TotalPrice = 100
                },
                new BillDto()
                {
                    Id = 3,
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
                    PaidPrice = 10,
                    BillExpiredTime = DateTime.Now.AddDays(1),
                    TotalPrice = 100
                },
                new BillDto()
                {
                    Id = 3,
                    Name = "دی",
                    ContractId = 1,
                    PaidPrice = 10,
                    BillExpiredTime = DateTime.Now.AddMonths(1),
                    TotalPrice = 100
                }
            };

            BillDto bill = bills.FirstOrDefault();
            PDFGenerator PDF = new PDFGenerator();
            PDF.TitlesPer = new List<Paragraph>()
            {
                new Paragraph("نمایش قبض".Fix())
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(24),
            };
            PDF.TblPer = new List<PDFTable>()
            {
                new PDFTable(2,60,HorizontalAlignment.CENTER)
                .AddRow("شمار قبض :",bill.Id)
                .AddRow("عنوان :",bill.Name)
                .AddRow("مبلق پرداخت شده :",bill.PaidPrice)
                .AddRow("مبلغ کل :",bill.TotalPrice)
                .AddRow("کامل پرداخت شده :",bill.HasPaid)
                .AddRow("وضعیت :",bill.HasPaid)
                .AddRow("تاریخ پرداخت :",bill.PaidTime)
            };
            return File(PDF.Generate(), "application/pdf");
        }
    }
}
