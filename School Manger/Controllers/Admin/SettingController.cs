using Microsoft.AspNetCore.Mvc;
using School_Manager.Core.Services.Interfaces;
using School_Manager.Core.ViewModels.FModels;

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
            return View("Index", _settingService.GetAllSetting());
        }
        [HttpGet]
        public IActionResult Edit(string key)
        {
            var setting = _settingService.GetAllSetting().FirstOrDefault(x => x.Key == key);
            if (setting == null)
            {
                ControllerExtensions.ShowError(this, "پیدا نشد", "تنظیمات مورد نظر یافت نشد.");
                return Index();
            }
            return View(setting);
        }
        [HttpPost]
        public IActionResult Edit(SettingDto dto)
        {
            if (ModelState.IsValid)
            {
                if (dto.Type == "Image")
                {
                    if (_settingService.SaveSettingImage(dto).Result)
                    {
                        ControllerExtensions.ShowSuccess(this, "ذخیره شد", "تنظیمات با موفقیت ذخیره شد.");
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    if (_settingService.SaveSetting(dto))
                    {
                        ControllerExtensions.ShowSuccess(this, "ذخیره شد", "تنظیمات با موفقیت ذخیره شد.");
                        return RedirectToAction("Index");
                    }
                }
            }
            ControllerExtensions.ShowError(this, "خطا", "خطایی در ذخیره سازی رخ داده است.");
            return View(dto);
        }
        [HttpGet]
        public IActionResult Delete(string key)
        {
            var setting = _settingService.GetAllSetting().FirstOrDefault(x => x.Key == key);
            if (setting == null)
            {
                ControllerExtensions.ShowError(this, "پیدا نشد", "تنظیمات مورد نظر یافت نشد.");
                return Index();
            }
            return View(setting);
        }
        [HttpPost]
        public IActionResult Delete(SettingDto dto)
        {
            SettingDto nulldto = new SettingDto
            {
                Key = dto.Key,
                Type = dto.Type,
                Value = null,
                File = null
            };
            dto = nulldto;
            if (_settingService.SaveSetting(dto))
            {
                ControllerExtensions.ShowSuccess(this, "حذف شد", "تنظیمات با موفقیت حذف شد.");
                return RedirectToAction("Index");
            }
            ControllerExtensions.ShowError(this, "خطا", "خطایی در حذف تنظیمات رخ داده است.");
            return View(dto);
        }
    }
}
