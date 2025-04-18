using Microsoft.AspNetCore.Mvc;

namespace School_Manger.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
