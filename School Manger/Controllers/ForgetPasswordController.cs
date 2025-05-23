using Microsoft.AspNetCore.Mvc;

namespace School_Manger.Controllers
{
    public class ForgetPasswordController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult OTPConfirmation()
        {
            return View();
        }
        public IActionResult ResetPassword()
        {
            return View();
        }
    }
}
