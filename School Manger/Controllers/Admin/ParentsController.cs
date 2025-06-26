using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School_Manager.Core.Services.Interfaces;
using School_Manager.Core.ViewModels.FModels;
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
        public ParentsController(IParentService parentService, IChildService childService, IContractService contractService, IBillService billService, ISchoolService schoolService, IDriverService driverService)
        {
            _parentService = parentService;
            _childService = childService;
            _contractService = contractService;
            _billService = billService;
            _schoolService = schoolService;
            _driverService = driverService;
        }
        public async Task<IActionResult> Index()
        {
            var Parents = await _parentService.GetParents();
            return View(Parents);
        }
        public IActionResult Details(int id)
        {
            var Model = _parentService.GetParent(id);
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
            return View(Child);
        }
        [HttpPost]
        public IActionResult MakeBill(long ChildId, string Name,long TotalPrice,string EndTime,string IsPaid, string TrackCode,string PaymentType,long PaidPrice)
        {
            var Child = _childService.GetChild(ChildId);
            return View("CreateBill", Child);
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
