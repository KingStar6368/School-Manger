using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School_Manager.Core.Services.Interfaces;
using School_Manger.PaymentService;
using System.Threading.Tasks;

namespace School_Manger.Controllers.Admin
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class PaysController : Controller
    {
        private IBillService _billService;
        private IZarinPalService _zarinPalService;
        public PaysController(IBillService billService, IZarinPalService zarinPalService)
        {
            _billService = billService;
            _zarinPalService = zarinPalService;
        }
        public async Task<IActionResult> Index()
        {
            return View("Index",await _billService.GetAllPays());
        }
        public async Task<JsonResult> Inquiry(string autority)
        {
            try
            {
                //VERIFIED: وریفای شده
                //PAID: پرداخت شده(وریفای نشده)
                //IN_BANK: درحال پرداخت
                //FAILED: ناموفق(تکمیل نشده)
                //REVERSED: تراکنش ریورس شده
                var result = await _zarinPalService.TransactionInquiry(autority);
                return new JsonResult(result);
            }
            catch
            {
                return new JsonResult(0);
            }
        }
    }
}
