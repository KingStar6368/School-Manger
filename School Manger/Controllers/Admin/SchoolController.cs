using Microsoft.AspNetCore.Mvc;
using School_Manager.Core.Services.Interfaces;
using School_Manager.Core.ViewModels.FModels;
using School_Manger.Extension;
using School_Manger.Models.PageView;
using System.Threading.Tasks;

namespace School_Manger.Controllers.Admin
{
    [Area("Admin")]
    public class SchoolController : Controller
    {
        private readonly ISchoolService _schoolService;
        private readonly IChildService _childService;
        private readonly IDriverService _driverService;
        public SchoolController(ISchoolService schoolService,IChildService childService,IDriverService driverService)
        {
           _schoolService = schoolService;
            _childService = childService;
            _driverService = driverService;
        }
        private static List<SchoolDto> _schools = new()
        {
            new SchoolDto {
                Id = 1,
                Name = "دبیرستان نمونه دولتی البرز",
                ManagerName = "دکتر محمدی",
                Rate = 4,
                Address = new LocationDataDto {
                    Address = "تهران، خیابان انقلاب",
                    Latitude = 35.7025,
                    Longitude = 51.4356
                }
            },
            new SchoolDto {
                Id = 2,
                Name = "مدرسه غیرانتفاعی مهر",
                ManagerName = "خانم رضایی",
                Rate = 5,
                Address = new LocationDataDto {
                    Address = "تهران، خیابان ولیعصر",
                    Latitude = 35.7152,
                    Longitude = 51.4053
                }
            }
        };

        public async Task<IActionResult> Index()
        {
            var Schools = await _schoolService.GetSchools();
            return View(Schools);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(SchoolDto model)
        {
            _schoolService.CreateSchool(new SchoolCreateDto()
            {
                Name = model.Name,
                ManagerName = model.ManagerName,
                Rate = 0,
                Address = model.Address.Address,
                Latitude = model.Address.Latitude,
                Longitude = model.Address.Longitude
            });
            ControllerExtensions.ShowSuccess(this, "موفق", "مدرسه با موفقعیت اضافه شد");
            return View(model);
        }
        public async Task<IActionResult> Details(long id)
        {
            var School = _schoolService.GetSchool(id);
            if (School == null)
                return await Index();
            var Drivers = await _driverService.GetDrivers(id);
            var Childern = await _schoolService.GetChildren(id);
            AdminSchool dashbord = new AdminSchool()
            {
                School = School,
                Drivers = Drivers,
                Students = Childern
            };
            return View(dashbord);
        }
    }
}
