using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace School_Manger.Controllers.Admin
{
    [Area("Admin")]
    public class ParentsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
