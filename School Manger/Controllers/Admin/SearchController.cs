using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School_Manager.Core.Services.Implemetations;
using School_Manager.Core.Services.Interfaces;
using School_Manager.Core.ViewModels.FModels;
using School_Manger.Extension;
using System.Threading.Tasks;

namespace School_Manger.Controllers.Admin
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class SearchController : Controller
    {
        private IParentService parentService;
        private IChildService childService;
        private IDriverService driverService;
        private IBillService billService;
        public SearchController(IChildService childService, IDriverService driverService,IParentService parentService,IBillService billService)
        {
            this.childService = childService;
            this.driverService = driverService;
            this.parentService = parentService;
            this.billService = billService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Driver(SearchDto searchDto)
        {
            return View(await driverService.SearchDriver(searchDto));
        }
        [HttpPost]
        public async Task<IActionResult> Parent(SearchDto searchDto)
        {
            return View(await parentService.SearchParents(searchDto));
        }
        [HttpPost]
        public async Task<IActionResult> Child(SearchDto searchDto)
        {
            return View(await childService.SearchChild(searchDto));
        }
        [HttpPost]
        public async Task<IActionResult> BilPays(SearchDto searchDto)
        {
            if(searchDto.MonthInt != null)
                searchDto.MonthInt = DateConverter.ShamsiMonthToMiladiMonth((int)searchDto.MonthInt);
            ViewBag.SearchDto = searchDto;
            return View(await billService.SearchBill(searchDto));
        }
        [HttpPost]
        public async Task<IActionResult> LoadMoreBills(SearchDto dto)
        {
            if (dto.MonthInt != null)
                dto.MonthInt = DateConverter.ShamsiMonthToMiladiMonth((int)dto.MonthInt);
            var result = await billService.SearchBill(dto);
            return PartialView("_BillRows", (dto.Page,result));
        }
    }
}
