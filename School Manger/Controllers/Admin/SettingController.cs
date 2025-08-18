using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School_Manager.Core.Services.Interfaces;
using School_Manager.Core.ViewModels.FModels;

namespace School_Manger.Controllers.Admin
{
    [Area("Admin")][Authorize(Roles = "Admin")]
    public class SettingController : Controller
    {
        private readonly ISettingService _settingService;
        public SettingController(ISettingService settingService)
        {
            _settingService = settingService;
        }
        public IActionResult Index()
        {
            return View("Index", _settingService.LoadSettingsFromDatabase());
        }
        [HttpPost]
        public IActionResult Edit(string Key, string Value, string Type, IFormFile IFile)
        {
            var dto = new SettingDto()
            {
                Key = Key,
                Type = Type,
                Value = Value,
                File = IFile
            };
            if (Type == "Image")
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
            ControllerExtensions.ShowError(this, "خطا", "خطایی در ذخیره سازی رخ داده است.");
            return Index();
        }
        [HttpGet]
        public IActionResult Delete(string Key)
        {
            var setting = _settingService.LoadSettingsFromDatabase().FirstOrDefault(x => x.Key == Key);
            SettingDto nulldto = new SettingDto
            {
                Key = setting.Key,
                Type = setting.Value.Item2,
                Value = null,
                File = null
            };
            if (nulldto.Type == "Image")
            {
                nulldto.Value = ""; // Clear the value for image type
                if (_settingService.SaveSettingImage(nulldto).Result)
                {
                    ControllerExtensions.ShowSuccess(this, "حذف شد", "تنظیمات با موفقیت حذف شد.");
                    return RedirectToAction("Index");
                }
            }
            else
            {
                nulldto.Value = ""; // Clear the value for text type
                if (_settingService.SaveSetting(nulldto))
                {
                    ControllerExtensions.ShowSuccess(this, "حذف شد", "تنظیمات با موفقیت حذف شد.");
                    return RedirectToAction("Index");
                }
            }
            ControllerExtensions.ShowError(this, "خطا", "خطایی در حذف تنظیمات رخ داده است.");
            return Index();
        }
    }
}
