using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Reporting.NETCore;
using School_Manager.Core.Services.Implemetations;
using School_Manager.Core.Services.Interfaces;
using School_Manager.Core.ViewModels.FModels;
using School_Manager.Domain.Entities.Catalog.Enums;
using School_Manager.Domain.Entities.Catalog.Operation;
using School_Manger.Class;
using School_Manger.Extension;
using School_Manger.Models.PageView;
using SMS.Base;
using SMS.TempLinkService;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace School_Manger.Controllers.Admin
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ParentsController : Controller
    {
        private readonly IUserService _userService;
        private readonly IParentService _parentService;
        private readonly IChildService _childService;
        private readonly IContractService _contractService;
        private readonly IBillService _billService;
        private readonly ISchoolService _schoolService;
        private readonly IDriverService _driverService;
        private readonly IPayBillService _payBillService;
        private readonly ITariffService _tariffService;
        private readonly IAppConfigService _appConfigService;
        private readonly ISMSService _smsService;
        private readonly ITempLink _tempLink;
        private readonly ISettingService _settingService;
        private readonly IWebHostEnvironment _env;
        public ParentsController(IParentService parentService, IChildService childService, IContractService contractService, 
            IBillService billService,ISchoolService schoolService, IDriverService driverService, IPayBillService payBillService,
            ITariffService tariffService,IWebHostEnvironment env,ITempLink appConfigService, ISMSService smsService,
            IAppConfigService appconfigService,IUserService userService,ISettingService settingService)
        {
            _parentService = parentService;
            _childService = childService;
            _contractService = contractService;
            _billService = billService;
            _schoolService = schoolService;
            _driverService = driverService;
            _payBillService = payBillService;
            _tariffService = tariffService;
            _tempLink = appConfigService;
            _env = env;
            _smsService = smsService;
            _appConfigService = appconfigService;
            _userService = userService;
            _settingService = settingService;
        }
        public async Task<IActionResult> Index()
        {
            var Parents = await _parentService.GetParents();
            return View(Parents);
        }
        public IActionResult Details(long id)
        {
            var Model = _parentService.GetParent(id);
            Model.Children = _childService.GetChildrenParent(Model.Id);
            List<DriverDto> drivers = new List<DriverDto>();
            List<SchoolDto> schools = new List<SchoolDto>();
            foreach (var child in Model.Children)
            {
                if (child.DriverId != null)
                    drivers.Add(_driverService.GetDriver((long)child.DriverId));
                if (child.SchoolId != null)
                    schools.Add(_schoolService.GetSchool((long)child.SchoolId));
            }
            AdminParent admindashbord = new AdminParent()
            {
                Parent = Model,
                Drivers = drivers,
                Schools = schools
            };
            return View("Details", admindashbord);
        }
        [HttpPost]
        public IActionResult CreateBill(long ChildId)
        {
            var Child = _childService.GetChild(ChildId);
            return View("CreateBill", Child);
        }
        [HttpPost]
        public IActionResult MakeBill(long ChildId, string Name, long TotalPrice, string StartTime, string EndTime, string Estimate, string IsPerBill)
        {
            if (IsPerBill == "on")
            {
                SavePreBillResult result = _billService.CreatePreBill(new CreatePreBillDto()
                {
                    ChildRef = ChildId,
                    Name = Name,
                    EndTime = EndTime.ToMiladi(),
                    EstimateTime = Estimate.ToMiladi(),
                    Price = TotalPrice,
                    StartTime = StartTime.ToMiladi()
                });
                var child = _childService.GetChild(ChildId);
                return CreateBill(ChildId);
            }
            else
            {
                long contractref = _contractService.GetContractWithChild(ChildId).Id;
                if (contractref == 0 || contractref == null)
                {
                    ControllerExtensions.ShowError(this, "خطا", "این دانش آموز قرار داد فعال ندارد");
                    return CreateBill(ChildId);
                }
                _billService.Create(new BillCreateDto()
                {
                    Name = Name,
                    Price = TotalPrice,
                    EstimateTime = Estimate.ToMiladi(),
                    ServiceContractRef = contractref,
                    Type = (int)BillType.Normal
                });
                var child = _childService.GetChild(ChildId);
                return CreateBill(ChildId);
            }
        }
        [HttpGet]
        public IActionResult BillCal(long Id)
        {
            var contractId = _contractService.GetContractWithChild(Id).Id;
            var child = _childService.GetChild(Id);
            var parent = _parentService.GetParentWithChild(Id);
            ControllerExtensions.AddObject(this, "Path", child.Path);
            ControllerExtensions.AddKey(this, "ChildId", Id);
            ControllerExtensions.AddKey(this, "ParentId", Id);
            return View(
            new BillCalViewModel()
            {
                Installment = new BillInstallmentDto()
                {
                    ServiceContractRef = contractId,
                },
                Location = child.Path,
                Bills = null
            });
        }
        [HttpPost]
        public IActionResult BillCalPerView(BillCalViewModel data, string PreStartDate, string PreEndDate)
        {
            data.Installment.StartDate = PreStartDate.ToMiladi();
            data.Installment.EndDate = PreEndDate.ToMiladi();
            data.Location = ControllerExtensions.GetKey<LocationPairModel>(this, "Path");
            data.Bills = _billService.Create(data.Installment);
            return View("BillCal", data);
        }
        [HttpPost]
        public IActionResult BillCal(BillInstallmentDto data)
        {
            var bills = _billService.Create(data);
            List<BillCreateDto> createDtos = new List<BillCreateDto>();
            foreach (BillDto bill in bills)
            {
                createDtos.Add(new BillCreateDto()
                {
                    Name = bill.Name,
                    EstimateTime = bill.BillExpiredTime,
                    Price = bill.TotalPrice,
                    ServiceContractRef = bill.ContractId,
                    Type = (int)BillType.Normal,
                });
            }
            if (_billService.Create(createDtos))
            {
                ControllerExtensions.ShowSuccess(this, "موفق", "قبض ها صادر شد");
                //Generate One use link with auto login to parent page and send it to parent
                long ParentId = ControllerExtensions.GetKey<long>(this,"ParentId");
                long ChildId = ControllerExtensions.GetKey<long>(this,"ChildId");
                 _smsService.Send(_userService.GetUserByParent(ParentId).Mobile, _tempLink.GenerateBillTempLink(ParentId, ChildId));
            }
            else
                ControllerExtensions.ShowSuccess(this, "خطا", "مشکلی در صادر قبض ها پیش آمده");
            long id = ControllerExtensions.GetKey<long>(this, "ChildId");
            return CreateBill(id);
        }
        [HttpPost]
        public async Task<IActionResult> ShowContractPDF(BillInstallmentDto data)
        {
            var ChildId = ControllerExtensions.GetKey<long>(this, "ChildId");
            var ParentId = ControllerExtensions.GetKey<long>(this, "ParentId");

            // دریافت لیست قبوض
            var bills = _billService.Create(data);
            bills.AddRange(await _billService.GetChildBills(ChildId));

            if (bills == null || bills.Count == 0)
                return BadRequest("No bill data.");

            // مسیر فایل RDLC
            string rdlcPath = Path.Combine(_env.WebRootPath, "reports", "ContractDlc.rdlc");

            // پارامترهای گزارش
            var parameters = new List<ReportParameter>
            {
                new ReportParameter("TotalPrice", bills.Sum(x => x.TotalPrice).ToString().ToMoney()),
                new ReportParameter("MonthPrice", data.Price.ToString().ToMoney()),
                new ReportParameter("PrePrice", bills.FirstOrDefault(x=>x.TypeOfBill == "پیش پرداخت").TotalPrice.ToString().ToMoney()),
            };
            List<ContractDlcTableData> TableData = new List<ContractDlcTableData>();
            int i = 1;
            foreach(var bill in bills)
            {
                TableData.Add(new ContractDlcTableData()
                {
                    Index = i++,
                    Amount = bill.TotalPrice.ToString().ToMoney() + " ریال",
                    BillName = bill.Name + " " + data.StartDate.ToPersain().Year.ToString(),
                    CurrentYear = data.StartDate.ToPersain().Year.ToString()
                });
            }
            byte[] pdfBytes;
            using (var report = new LocalReport())
            {
                using var fs = new FileStream(rdlcPath, FileMode.Open, FileAccess.Read);
                report.LoadReportDefinition(fs);

                // ⚡ این قسمت مهم است → اضافه کردن لیست قبوض به دیتا سورس
                report.DataSources.Add(new ReportDataSource("Month", TableData));

                // تنظیم پارامترها
                report.SetParameters(parameters);

                // خروجی PDF
                pdfBytes = report.Render("PDF");
            }

            return File(pdfBytes, "application/pdf", "Contract.pdf");
        }
        [HttpPost]
        public async Task<IActionResult> ShowContract2PDF()
        {
            var ChildId = ControllerExtensions.GetKey<long>(this, "ChildId");

            var Parent = _parentService.GetParentWithChild(ChildId);
            var Child = _childService.GetChild(ChildId);
            var User = _userService.GetUserByParent(Parent.Id);
            // مسیر فایل RDLC
            string rdlcPath = Path.Combine(_env.WebRootPath, "reports", "ContractDlc2.rdlc");

            // پارامترهای گزارش
            var parameters = new List<ReportParameter>
            {
                new ReportParameter("OwnerName", _settingService.Get("CompanyOwner")),
                new ReportParameter("OwnerNationCode", _settingService.Get("CompanyOwnerNationCode")),
                new ReportParameter("CompanyName", _settingService.Get("CompanyName")),
                new ReportParameter("StudentName", Child.FirstName),
                new ReportParameter("StudentClass", Child.Class),
                new ReportParameter("ParentName", Parent.ParentFirstName),
                new ReportParameter("ParentNationcode", Parent.ParentNationalCode),
                new ReportParameter("PhoneNumber", User.Mobile),
                new ReportParameter("StudentDate", DateTime.Now.ToPersain().Year.ToString() + "-" + DateTime.Now.AddYears(1).ToPersain().Year.ToString()),
            };
            byte[] pdfBytes;
            using (var report = new LocalReport())
            {
                using var fs = new FileStream(rdlcPath, FileMode.Open, FileAccess.Read);
                report.LoadReportDefinition(fs);

                // تنظیم پارامترها
                report.SetParameters(parameters);

                // خروجی PDF
                pdfBytes = report.Render("PDF");
            }

            return File(pdfBytes, "application/pdf", "Contract2.pdf");
        }

        [HttpPost]
        public IActionResult PayBill(long ChildId, long BillId, string TrackCode, string PaymentType, long PaidPrice, string PiadTime)
        {
            var child = _childService.GetChild(ChildId);
            BillDto Bill = _billService.GetBill(BillId);
            if (Bill == null)
                return CreateBill(ChildId);
            if (Bill.HasPaid)
                return CreateBill(ChildId);
            PayType type = PayType.Pos;
            switch (PaymentType)
            {
                case "Cash":
                    type = PayType.Cash;
                    break;
                case "Pos":
                    type = PayType.Pos;
                    break;
                case "Internet":
                    type = PayType.Internet;
                    break;
            }
            _payBillService.CreatePay(new PayCreateDto()
            {
                Bills = new List<long>()
                {
                    Bill.Id,
                },
                Price = PaidPrice,
                BecomingTime = PiadTime.ToMiladi(),
                PayType = type
            });
            ControllerExtensions.ShowSuccess(this, "موفق", "پرداخت انجام شد");
            return CreateBill(ChildId);
        }
        public IActionResult DeleteBill(long Id, long secondId)
        {
            try
            {
                if (_billService.Delete(Id))
                    ControllerExtensions.ShowSuccess(this, "موفق", "قبض حذف شد");
                else
                    ControllerExtensions.ShowError(this, "خطا", "مشکلی پیش آمده");
            }
            catch (Exception ex)
            {
                ControllerExtensions.ShowError(this, "خطا", ex.Message);
            }
            return CreateBill(secondId);
        }
        public IActionResult DeleteChild(long Id, long PId)
        {
            try
            {
                if (_childService.DeleteChild(Id))
                    ControllerExtensions.ShowSuccess(this, "موفق", "فرزند حذف شد");
                else
                    ControllerExtensions.ShowError(this, "خطا", "مشکلی پیش آمده");
            }
            catch (Exception ex)
            {
                ControllerExtensions.ShowError(this, "خطا", "این فرزند داده های وابسته دارد امکان حذف آن نیست");
            }
            return Details(PId);
        }
        [HttpGet]
        public IActionResult EditBill(long billId, long childId)
        {
            var bill = _billService.GetBill(billId);
            if (bill == null)
                return NotFound();
            ViewBag.ChildId = childId;
            return View("EditBill", bill);
        }

        [HttpPost]
        public IActionResult EditBill(long billId, long childId, BillUpdateDto model, string EstimateTime)
        {
            try
            {
                model.EstimateTime = EstimateTime.ConvertPersianToEnglish().ToMiladi();
                _billService.Update(model);
                ControllerExtensions.ShowSuccess(this, "موفق", "تغییرات اعمال شد");
                return CreateBill(childId);
            }
            catch
            {
                ViewBag.ChildId = childId;
                return View("EditBill", model);
            }
        }
        [HttpGet]
        public async Task<IActionResult> UpdateChild(long id, long parentId)
        {
            var child = _childService.GetChild(id);
            if (child == null)
                return NotFound();
            var model = new ChildUpdateDto
            {
                Id = child.Id,
                ParentRef = parentId,
                SchoolRef = child.SchoolId,
                FirstName = child.FirstName,
                LastName = child.LastName,
                NationalCode = child.NationalCode.ConvertPersianToEnglish(),
                BirthDate = child.BirthDate,
                Class = int.TryParse(child.Class, out var c) ? c : 0,
                LocationPairs = new List<LocationPairUpdateDto>()
                {
                    new()
                    {
                        Id = child.Path.Id,
                        ChildRef = child.Path.ChildId,
                        PickTime1 = child.Path.PickTime1,
                        PickTime2 = child.Path.PickTime2,
                        Locations = new List<LocationDataUpdateDto>()
                        {
                            new()
                            {
                                Id = child.Path.Location1.Id,
                                Address = child.Path.Location1.Address,
                                IsActive = true,
                                Latitude = child.Path.Location1.Latitude,
                                Longitude = child.Path.Location1.Longitude,
                                LocationType = LocationType.Start,
                            },
                            new()
                            {
                                Id = child.Path.Location2.Id,
                                Address = child.Path.Location2.Address,
                                IsActive = true,
                                Latitude = child.Path.Location2.Latitude,
                                Longitude = child.Path.Location2.Longitude,
                                LocationType = LocationType.End,
                            }
                        }
                    }
                }
            };
            ViewBag.ParentId = parentId;
            ViewBag.Schools = await _schoolService.GetSchools();
            return View("EditChild", model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateChild(ChildUpdateDto model, long parentId, string BirthDate)
        {
            try
            {
                model.LocationPairs[0].Locations[0].IsActive = true;
                model.LocationPairs[0].Locations[1].IsActive = true;
                model.NationalCode = model.NationalCode.ConvertPersianToEnglish();
                model.BirthDate = BirthDate.ConvertEnglishToPersian().ToMiladi();
                var result = _childService.UpdateChild(model);
                if (result)
                {
                    ControllerExtensions.ShowSuccess(this, "موفق", "تغییرات فرزند ذخیره شد");
                    return RedirectToAction("Details", new { id = parentId });
                }
                else
                {
                    ControllerExtensions.ShowError(this, "خطا", "خطا در ذخیره تغییرات");
                }
            }
            catch
            {
            }
            ViewBag.ParentId = parentId;
            ViewBag.Schools = await _schoolService.GetSchools();
            return View("EditChild", model);
        }
        [HttpPost]
        public JsonResult GetPriceByKm([FromBody] float km)
        {
            var tariffsTask = _tariffService.GetActiveTariff();
            tariffsTask.Wait();
            var tariffs = tariffsTask.Result;
            decimal kmDecimal = (decimal)km;
            var tariff = tariffs.FirstOrDefault(t => kmDecimal >= t.FromKilometer && kmDecimal <= t.ToKilometer);
            int price = tariff != null ? tariff.Price : 0;
            return Json(new { price });
        }
        [HttpGet]
        public async Task<JsonResult> GetKm(float fromLat, float fromLon, float toLat, float toLon)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    // Build the API URL with the provided coordinates
                    string apiUrl = _appConfigService.ApiUrl()+$"/Route?fromLat={fromLat}&fromLon={fromLon}&toLat={toLat}&toLon={toLon}";

                    // Make the GET request to the external API
                    HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

                    // Ensure the request was successful
                    response.EnsureSuccessStatusCode();

                    // Read the response content as string
                    string responseContent = await response.Content.ReadAsStringAsync();

                    // Return the JSON response from the external API
                    return new JsonResult(responseContent);
                }
            }
            catch (HttpRequestException ex)
            {
                // Handle HTTP request errors
                return new JsonResult(new { error = $"API request failed: {ex.Message}" })
                {
                    StatusCode = 500
                };
            }
            catch (Exception ex)
            {
                // Handle other errors
                return new JsonResult(new { error = $"An error occurred: {ex.Message}" })
                {
                    StatusCode = 500
                };
            }
        }
    }
}
