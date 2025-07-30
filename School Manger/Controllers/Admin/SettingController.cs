using Microsoft.AspNetCore.Mvc;
using ServiceBase.Setting;
using System.Linq;
using System.IO;

namespace School_Manger.Controllers.Admin
{
    [Area("Admin")]
    public class SettingController : Controller
    {
        private readonly ISettingService _settingService;
        public SettingController(ISettingService settingService)
        {
            _settingService = settingService;
        }
        public IActionResult Index()
        {
            // Get all SettingType values except None
            var settingTypes = System.Enum.GetValues(typeof(SettingType)).Cast<SettingType>()
                .Where(x => x != SettingType.None).ToList();
            var settings = settingTypes
                .Select(type => _settingService.GetSettingDto(type))
                .Where(dto => dto != null)
                .ToList();
            return View(settings);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new SettingDto());
        }

        [HttpPost]
        public IActionResult Create(SettingDto model)
        {
            if (model.SettingType == SettingType.CompanyLogo && Request.Form.Files.Count > 0)
            {
                var file = Request.Form.Files["LogoFile"];
                if (file != null && file.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                        model.Value = ms.ToArray();
                        model.ValueType = typeof(byte[]);
                    }
                }
            }
            else
            {
                model.ValueType = typeof(string);
            }
            _settingService.SetSetting(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(SettingType id)
        {
            var setting = _settingService.GetSettingDto(id);
            if (setting == null) return NotFound();
            return View(setting);
        }

        [HttpPost]
        public IActionResult Edit(SettingDto model)
        {
            if (model.SettingType == SettingType.CompanyLogo && Request.Form.Files.Count > 0)
            {
                var file = Request.Form.Files["LogoFile"];
                if (file != null && file.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                        model.Value = ms.ToArray();
                        model.ValueType = typeof(byte[]);
                    }
                }
            }
            else
            {
                model.ValueType = typeof(string);
            }
            _settingService.SetSetting(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(SettingType id)
        {
            var setting = _settingService.GetSettingDto(id);
            if (setting == null) return NotFound();
            return View(setting);
        }

        [HttpPost]
        public IActionResult Delete(SettingDto model)
        {
            _settingService.DeleteSetting(model.SettingType);
            return RedirectToAction("Index");
        }
    }
}
