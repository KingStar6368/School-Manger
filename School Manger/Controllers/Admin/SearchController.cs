using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School_Manager.Core.Services.Interfaces;
using School_Manager.Core.ViewModels.FModels;
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
        public SearchController(IChildService childService, IDriverService driverService,IParentService parentService)
        {
            this.childService = childService;
            this.driverService = driverService;
            this.parentService = parentService;
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
    }
}
