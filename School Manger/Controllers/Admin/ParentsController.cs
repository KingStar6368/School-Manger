using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School_Manager.Core.Services.Interfaces;
using School_Manager.Core.ViewModels.FModels;
using School_Manager.Domain.Entities.Catalog.Enums;
using School_Manger.Extension;
using School_Manger.Models.PageView;

namespace School_Manger.Controllers.Admin
{
    [Area("Admin")]
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
        public ParentsController(IParentService parentService, IChildService childService, IContractService contractService, IBillService billService,
            ISchoolService schoolService, IDriverService driverService, IPayBillService payBillService)
        {
            _parentService = parentService;
            _childService = childService;
            _contractService = contractService;
            _billService = billService;
            _schoolService = schoolService;
            _driverService = driverService;
            _payBillService = payBillService;
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
            return View("Details",admindashbord);
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
        public IActionResult DeleteChild(long Id,long PId)
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
        public IActionResult EditBill(long billId, long childId, BillUpdateDto model)
        {
            if (ModelState.IsValid)
            {
                _billService.Update(model);
                ControllerExtensions.ShowSuccess(this, "موفق", "تغییرات اعمال شد");
                return CreateBill(childId);
            }
            ViewBag.ChildId = childId;
            return View("EditBill", model);
        }
        [HttpGet]
        public IActionResult UpdateChild(long id, long parentId)
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
                NationalCode = child.NationalCode,
                BirthDate = child.BirthDate,
                Class = int.TryParse(child.Class, out var c) ? c : 0
            };
            ViewBag.ParentId = parentId;
            return View("EditChild", model);
        }

        [HttpPost]
        public IActionResult UpdateChild(ChildUpdateDto model, long parentId)
        {
            if (ModelState.IsValid)
            {
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
            ViewBag.ParentId = parentId;
            return View("EditChild", model);
        }
    }
}
