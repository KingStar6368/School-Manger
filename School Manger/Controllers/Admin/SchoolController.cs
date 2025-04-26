using Microsoft.AspNetCore.Mvc;
using School_Manger.Models;

namespace School_Manger.Controllers.Admin
{
    [Area("Admin")]
    public class SchoolController : Controller
    {
        private static List<School> _schools = new()
    {
        new School {
            Id = 1,
            Name = "دبیرستان نمونه دولتی البرز",
            ManagerName = "دکتر محمدی",
            Rate = 4,
            Address = new LocationData {
                Address = "تهران، خیابان انقلاب",
                Latitude = 35.7025,
                Longitude = 51.4356
            }
        },
        new School {
            Id = 2,
            Name = "مدرسه غیرانتفاعی مهر",
            ManagerName = "خانم رضایی",
            Rate = 5,
            Address = new LocationData {
                Address = "تهران، خیابان ولیعصر",
                Latitude = 35.7152,
                Longitude = 51.4053
            }
        }
    };

        public IActionResult Index()
        {
            return View(_schools);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(School model)
        {
            if (ModelState.IsValid)
            {
                model.Id = _schools.Max(s => s.Id) + 1;
                _schools.Add(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}
