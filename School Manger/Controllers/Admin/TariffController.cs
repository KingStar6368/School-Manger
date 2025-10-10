using Microsoft.AspNetCore.Mvc;
using School_Manager.Core.Services.Interfaces;
using School_Manager.Core.ViewModels.FModels;
using System.Threading.Tasks;
using System.Linq;
using School_Manger.Extension;
using Microsoft.AspNetCore.Authorization;

namespace School_Manger.Controllers.Admin
{
    [Area("Admin")][Authorize(Roles = "Admin")]
    public class TariffController : Controller
    {
        private readonly ITariffService _tariffService;
        public TariffController(ITariffService tariffService)
        {
            _tariffService = tariffService;
        }
        public async Task<IActionResult> Index()
        {
            var tariffs = await _tariffService.GetActiveTariff();
            return View(tariffs);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new TariffDto());
        }

        [HttpPost]
        public IActionResult Create(TariffDto model, string StartDate, string EndDate)
        {
            try
            {
                model.FromDate = StartDate.ToMiladi();
                model.ToDate = EndDate.ToMiladi();
                int Result = _tariffService.CreateTariff(model);
                if (Result != null&&Result != 0)
                    ControllerExtensions.ShowSuccess(this, "موفق", "تعرفه ساخته شد");
                else
                    ControllerExtensions.ShowError(this, "ناموفق", "مشکلی در ساخت تعرفه پیش آمده");
                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id, string StartDate, string EndDate)
        {
            var tariffs = await _tariffService.GetActiveTariff();
            var tariff = tariffs.FirstOrDefault(x => x.Id == id);
            if (tariff == null) return NotFound();
            return View(tariff);
        }

        [HttpPost]
        public IActionResult Edit(TariffDto model, string StartDate, string EndDate)
        {
            try
            {
                model.FromDate = StartDate.ToMiladi();
                model.ToDate = EndDate.ToMiladi();
                bool Result = _tariffService.UpdateTariff(model);
                if (Result)
                    ControllerExtensions.ShowSuccess(this, "موفق", "تعرفه تغییرات اعمال شد");
                else
                    ControllerExtensions.ShowError(this, "ناموفق", "مشکلی در تغییر تعرفه پیش آمده");
                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var tariffs = await _tariffService.GetActiveTariff();
            var tariff = tariffs.FirstOrDefault(x => x.Id == id);
            if (tariff == null) return NotFound();
            return View(tariff);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            bool Reuslt = _tariffService.DeleteTariff(id);
            if(Reuslt)
                ControllerExtensions.ShowSuccess(this, "موفق", "تعرفه حذف شد");
            else
                ControllerExtensions.ShowError(this, "ناموفق", "مشکلی در حذف پیش آمده");
            return RedirectToAction("Index");
        }
    }
}
