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
        public IActionResult Details(int id)
        {
            var Model = _parentService.GetParent(id);
            Model.Children = _childService.GetChildrenParent(Model.Id);
            List<DriverDto> drivers = new List<DriverDto>();
            List<SchoolDto> schools = new List<SchoolDto>();
            foreach(var child in Model.Children)
            {
                if (child.DriverId != null)
                    drivers.Add(_driverService.GetDriver((long)child.DriverId));
                if(child.SchoolId != null)
                    schools.Add(_schoolService.GetSchool((long)child.SchoolId));
            }
            AdminParent admindashbord = new AdminParent()
            {
                Parent = Model,
                Drivers = drivers,
                Schools = schools
            };
            return View(admindashbord);
        }
        [HttpPost]
        public IActionResult CreateBill(long ChildId)
        {
            var Child = _childService.GetChild(ChildId);
            return View("CreateBill", Child);
        }
        [HttpPost]
        public IActionResult MakeBill(long ChildId, string Name,long TotalPrice,string StartTime,string EndTime,string Estimate, string IsPerBill)
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
        public IActionResult PayBill(long ChildId,long BillId,string TrackCode,string PaymentType,long PaidPrice,string PiadTime)
        {
            var child = _childService.GetChild(ChildId);
            BillDto Bill = _billService.GetBill(BillId);
            if(Bill ==null)
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
        //[HttpPost]
        //public IActionResult test(string Child)
        //{
        //    return StatusCode(200);
        //}
        //<iframe>
        //<form target = iframe name
    }
}
