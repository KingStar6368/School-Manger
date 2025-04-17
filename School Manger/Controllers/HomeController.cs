using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using School_Manger.Models;

namespace School_Manger.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignIn()
        {
            return View("OTPConfirmation");
        }
        [HttpPost]
        public IActionResult VerifyOtp()
        {
            return View("UserInfo");
        }
        public IActionResult Login()
        {
            return View("Login");
        }
        [HttpPost]
        public IActionResult CompleteProfile()
        {
            return View("ParentMenu", new ParentWithChildrenViewModel()
            {
                Children = new List<ChildInfo>()
                {
                    new ChildInfo()
                    {
                        FirstName = "تست",
                        LastName = "test",
                        NationalCode = "0521744407",
                        BirthDate = DateTime.Now,
                        HasPaid = true,
                        Id = "1",
                        Price = 2000,
                    },
                      new ChildInfo()
                    {
                        FirstName = "تست",
                        LastName = "test",
                        NationalCode = "0521744407",
                        BirthDate = DateTime.Now,
                        HasPaid = false,
                        Id = "2",
                        Price = 100000
                    }
                },
                ParentFirstName = "اقای تست",
                ParentNationalCode = "0521744407",
                ParentLastName = "تست"

            });
        }
        [HttpPost]
        public IActionResult LocationSelector()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddChild(LocationPairModel model)
        {
            //Add Child And Show Menu Again
            return View("ParentMenu");
        }
    }
}
