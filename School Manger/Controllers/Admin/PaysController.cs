using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School_Manager.Core.Services.Interfaces;
using System.Threading.Tasks;

namespace School_Manger.Controllers.Admin
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class PaysController : Controller
    {
        private IBillService _billService;
        public PaysController(IBillService billService)
        {
            _billService = billService;
        }
        public async Task<IActionResult> Index()
        {
            return View("Index",await _billService.GetAllPays());
        }
    }
}
