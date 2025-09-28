using DNTPersianUtils.Core;
using iText.Layout.Element;
using iText.Layout.Properties;
using King;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Reporting.NETCore;
using PersianTextShaper;
using School_Manager.Core.Services.Implemetations;
using School_Manager.Core.Services.Interfaces;
using School_Manager.Core.ViewModels.FModels;
using School_Manager.Domain.Entities.Catalog.Enums;
using School_Manger.Class;
using School_Manger.Extension;
using School_Manger.Models;
using School_Manger.Models.PageView;
using School_Manger.PaymentService;
using SMS.Base;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;

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
        private readonly IWebHostEnvironment _env;
        private readonly ISettingService settingService;
        private readonly IPayment PaymentService;
        private readonly IAppConfigService AppConfigService;
        public HomeController(IParentService PService,IChildService CService,
            IUserService UService,IBillService billService,
            ISchoolService schoolService,ISMSService sMSService, IWebHostEnvironment env,ISettingService _settingservice,
            IPayment _payment,IAppConfigService appConfigService/*,IDriverService driverService,IContractService contractService*/)
        {
            //_DriverService = driverService;
            _PService = PService;
            _CService = CService;
            _UserService = UService;
            _BillService = billService;
            _Sservice = schoolService;
            SMSService = sMSService;
            _env = env;
            settingService = _settingservice;
            PaymentService = _payment;
            AppConfigService = appConfigService;
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
            ControllerExtensions.AddKey(this, "PhoneNumber", PhoneNumber);
            if (_UserService.IsMobileRegistered(ControllerExtensions.GetKey<string>(this, "PhoneNumber")))
            {
                ControllerExtensions.ShowError(this,"خطا", "این شماره موبایل در سیستم موجود است");
                return Redirect("Index");
            }
            //send code
            int RandomCode = new Random().Next(111111, 999999);
            ControllerExtensions.AddKey(this, "Code", RandomCode);
            if (!AppConfigService.SMSOtp())
                return View("UserInfo");
            if (!SMSService.Send(PhoneNumber, $"والد گرامی کد تایدد شما \n" + RandomCode))
            {
                ControllerExtensions.ShowError(this, "خطا", "کد تاییدی به شماره ارسال نشد");
                return View("Index");
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
        public async Task<IActionResult> Login(string NationalCode, string Password)
        {
            if (NationalCode == null && Password == null)
                return View();

            UserDTO user = _UserService.CheckAuthorize(NationalCode, Password);
            if (user == null)
            {
                ControllerExtensions.ShowError(this, "خطا در ورود", "کد ملی یا رمز عبور صحیح نیست!");
                return View();
            }
            ControllerExtensions.AddKey(this, "Uref", user.Id);
            // Create claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim("FullName", $"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.Role, user.Type.ToString())
            };

            // Add additional references based on user type
            switch (user.Type)
            {
                case UserType.Parent:
                    var parent = _PService.GetParentByNationCode(NationalCode);
                    claims.Add(new Claim("ParentId", parent.Id.ToString()));
                    ControllerExtensions.AddKey(this, "Pref", parent.Id);
                    break;

                case UserType.Admin:
                    claims.Add(new Claim("AdminId", "Admin")); // Or actual admin ID if available
                    break;
            }

            // Create identity and principal
            var claimsIdentity = new ClaimsIdentity(claims, "CookieAuth");
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            // Sign in
            await HttpContext.SignInAsync("CookieAuth", claimsPrincipal);

            // Redirect based on role
            switch (user.Type)
            {
                case UserType.Parent:
                    return ParentMenu();

                case UserType.Admin:
                    return RedirectToAction("Index", "Parents", new { area = "Admin" });

                default:
                    return View();
            }
        }
        [HttpPost]
        public IActionResult CompleteProfile(string nationalCode, string firstName, string lastName, string password)
        {
            nationalCode = nationalCode.ConvertPersianToEnglish();
            password = password.ConvertPersianToEnglish();
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
                    NationalCode = model.SelectedChild.NationalCode.ConvertPersianToEnglish(),
                    ParentRef = ControllerExtensions.GetKey<long>(this,"Pref"),
                    BirthDate = model.SelectedChild.BirthDate,
                    Class = int.Parse(model.SelectedChild.Class),//todo it must cast to int
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
        public IActionResult ShowBillPDF(long BillId)
        {
            var bill = _BillService.GetBill(BillId);
            if (bill == null)
                return BadRequest("No bill data.");

            // Path to the RDLC file
            string rdlcPath = Path.Combine(_env.WebRootPath, "reports", "Bill.rdlc");

            // Prepare report parameters
            var parameters = new List<ReportParameter>
            {
                new ReportParameter("Title", bill.Name),
                new ReportParameter("TotalPrice", bill.TotalPrice.ToString().ToMoney() + " ریال"),
                new ReportParameter("PaidPrice", bill.PaidPrice.ToString().ToMoney() + " ریال"),
                new ReportParameter("Estimatetime", bill.BillExpiredTime.ToPersianString()),
                new ReportParameter("Status", bill.HasPaid ? "پرداخت شده" : "پرداخت نشده")
            };

            // Load and render report
            byte[] pdfBytes;
            using (var report = new LocalReport())
            {
                using var fs = new FileStream(rdlcPath, FileMode.Open, FileAccess.Read);
                report.LoadReportDefinition(fs);

                // NOTE: Add your data source name and object here
                // For example:
                // report.DataSources.Add(new ReportDataSource("BillDataSet", new List<BillModel> { bill }));

                report.SetParameters(parameters);
                pdfBytes = report.Render("PDF");
            }

            return File(pdfBytes, "application/pdf", "Bill.pdf");
        }
        [HttpPost]  
        public IActionResult PayBill(long BillId)
        {
            BillDto bill = _BillService.GetBill(BillId);
            if (bill == null || bill.HasPaid)
            {
                ControllerExtensions.ShowError(this, "خطا", "قبض پیدا نشد");
                return ParentMenu();
            }
            var payment = new Payment((int)(bill.TotalPrice - bill.PaidPrice));
            var respance = payment.PaymentRequest("پرداخت قبض " + bill.Name, settingService.Get("PayUrl"));
            if(respance.Result.code == 100)
            {
                PaymentService.Add(new PayData()
                {
                    Autratory = respance.Result.authority,
                    BillIds = new List<long>()
                    {
                        bill.Id
                    }
                });
                return Redirect("https://sandbox.zarinpal.com/pg/StartPay/"+respance.Result.authority);
            }
            ControllerExtensions.ShowError(this, "خطا", "مشکلی در انتفال به درگاه شده لطفا بعدا امتحان کنید");
            return ParentMenu();
        }
    }
}
