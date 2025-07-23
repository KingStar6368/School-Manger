using System.Diagnostics;
using System.Threading.Tasks;
using DNTPersianUtils.Core;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.AspNetCore.Mvc;
using PersianTextShaper;
using School_Manager.Core.Services.Implemetations;
using School_Manager.Core.Services.Interfaces;
using School_Manager.Core.ViewModels.FModels;
using School_Manager.Domain.Entities.Catalog.Enums;
using School_Manger.Class;
using School_Manger.Extension;
using School_Manger.Models;
using School_Manger.Models.PageView;
using SMS.Base;

namespace School_Manger.Controllers
{
    public class HomeController : Controller
    {
        //static data
        private readonly IParentService _PService;
        private readonly IChildService _CService;
        private readonly IUserService _UserService;
        private readonly IBillService _BillService;
        private readonly ISchoolService _Sservice;
        //private readonly IDriverService _DriverService;
        //private readonly IContractService _ContractService;
        private readonly ISMSService SMSService;
        public HomeController(IParentService PService,IChildService CService,
            IUserService UService,IBillService billService,ISchoolService schoolService,ISMSService sMSService/*,IDriverService driverService,IContractService contractService*/)
        {
            //_DriverService = driverService;
            _PService = PService;
            _CService = CService;
            _UserService = UService;
            _BillService = billService;
            _Sservice = schoolService;
            SMSService = sMSService;
            //_ContractService = contractService;
        }

        #region Login&SignIn
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignIn(string PhoneNumber)
        {
            int RandomCode = new Random().Next(111111, 999999);
            ControllerExtensions.AddKey(this, "Code", RandomCode);
            if (!SMSService.Send(30007732008772, PhoneNumber, $"والد گرامی کد تایدد شما \n" + RandomCode))
            {
                ControllerExtensions.ShowError(this, "خطا", "کد تاییدی به شماره ارسال نشد");
                return View("Index");
            }
            ControllerExtensions.AddKey(this, "PhoneNumber", PhoneNumber);
            if (_UserService.IsMobileRegistered(ControllerExtensions.GetKey<string>(this, "PhoneNumber")))
            {
                ControllerExtensions.ShowError(this,"خطا", "این شماره موبایل در سیستم موجود است");
                return Redirect("Index");
            }
            //Otp
            return View("OTPConfirmation");
        }
        [HttpPost]
        public IActionResult VerifyOtp(int otpCode)
        {
            otpCode = int.Parse(new string(otpCode.ToString().Reverse().ToArray()));
            int Code = ControllerExtensions.GetKey<int>(this, "Code");
            if(Code == otpCode)
                return View("UserInfo");
            ControllerExtensions.ShowError(this, "خطا", "کد وارد شده اشتباه است");
            return View("OTPConfirmation");
        }
        public IActionResult Login(string NationalCode, string Password)
        {
            if(NationalCode == null && Password == null) 
                return View();
            else
            {
                UserDTO user = _UserService.CheckAuthorize(NationalCode, Password);
                if (user != null)
                {
                    ControllerExtensions.AddKey(this,"Uref",user.Id);
                    ControllerExtensions.AddKey(this,"Pref", _PService.GetParentByNationCode(NationalCode).Id);
                    return ParentMenu();
                }
                else
                {
                    ControllerExtensions.ShowError(this, "خطا در ورود", "کد ملی یا رمز عبور صحیح نیست!");
                    return View();
                }
            }
        }
        [HttpPost]
        public IActionResult CompleteProfile(string nationalCode, string firstName, string lastName, string password)
        {
            long UseRref = _UserService.CreateUser(new UserCreateDTO()
            {
                FirstName = firstName,
                LastName = lastName,
                IsActive = true,
                Mobile = ControllerExtensions.GetKey<string>(this, "PhoneNumber"),
                PasswordHash = password,
                UserName = nationalCode
            });
            long ParentRef = _PService.CreateParent(new ParentCreateDto()
            {
                FirstName = firstName,
                LastName = lastName,
                NationalCode = nationalCode,
                UserRef = UseRref,
                Active = true,
                Address = "",
            });
            ControllerExtensions.AddKey(this,"Uref",UseRref);
            ControllerExtensions.AddKey(this,"Pref", ParentRef);
            // Redirect to Login With Message موفق 
            return View("Login");
        }
        #endregion

        #region AfterLogin
        public IActionResult ParentMenu()
        {
            long Uref = ControllerExtensions.GetKey<long>(this,"Uref");
            long Pref = ControllerExtensions.GetKey<long>(this, "Pref");
            ParentDto parent = _PService.GetParent(Pref);
            parent.Children = _CService.GetChildrenParent(parent.Id);
            return View("ParentMenu", new ParentDashbordView()
            {
                Parent = parent,
            });
        }
        [HttpPost]
        public async Task<IActionResult> LocationSelector(ParentDashbordView view,string Date)
        {
            view.SelectedChild.BirthDate = Date.ConvertPersianToEnglish().ToMiladi();
            view.Schools = await _Sservice.GetSchools();
            return View(view);
        }
        [HttpPost]
        public IActionResult AddChild(ParentDashbordView model)
        {
            if(_CService.GetChildByNationCode(model.SelectedChild.NationalCode)!= null)
            {
                ControllerExtensions.ShowError(this, "خطا", "این فرزند وجود دارد");
                return ParentMenu();
            }
            else
            {
                _CService.CreateChild(new ChildCreateDto()
                {
                    FirstName = model.SelectedChild.FirstName,
                    LastName = model.SelectedChild.LastName,
                    NationalCode = model.SelectedChild.NationalCode,
                    ParentRef = ControllerExtensions.GetKey<long>(this,"Pref"),
                    BirthDate = model.SelectedChild.BirthDate,
                    Class = 1,//todo it must cast to int
                    SchoolRef = model.SelectedChild.SchoolId,
                    LocationPairs = new List<LocationPairCreateDto>()
                    {
                        new LocationPairCreateDto()
                        {
                            PickTime1 = model.SelectedChild.Path.PickTime1,
                            PickTime2 = model.SelectedChild.Path.PickTime2,
                            Locations = new List<LocationDataCreateDto>()
                            {
                                new LocationDataCreateDto()
                                {
                                    Address = model.SelectedChild.Path.Location1.Address,
                                    Latitude = model.SelectedChild.Path.Location1.Latitude,
                                    Longitude = model.SelectedChild.Path.Location1.Longitude,
                                    LocationType = LocationType.Start,
                                },
                                new LocationDataCreateDto()
                                {
                                    Address = model.SelectedChild.Path.Location2.Address,
                                    Latitude = model.SelectedChild.Path.Location2.Latitude,
                                    Longitude = model.SelectedChild.Path.Location2.Longitude,
                                    LocationType = LocationType.End,
                                }
                            }
                        }
                    }
                });
                ControllerExtensions.ShowSuccess(this, "موفق", "فرزند جدید اضافه شد");
            }
            return ParentMenu();
        }
        [HttpPost]
        public IActionResult RemoveChild(ParentDashbordView model)
        {
            _CService.DeleteChild(model.SelectedChild.Id);
            ControllerExtensions.ShowSuccess(this, "موفق", "فرزند حذف شد");
            return ParentMenu();
        }
        #endregion

        [HttpPost]
        public async Task<IActionResult> Bills(long Id)
        {
            ParentDto parent = _PService.GetParent(ControllerExtensions.GetKey<long>(this,"Pref"));
            ChildInfo child = _CService.GetChild(Id);
            List<BillDto> bills = await _BillService.GetChildBills(Id);
            BillDashbord dashbord = new BillDashbord()
            {
                bills = bills,
                child = child,
                parent = parent,
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
