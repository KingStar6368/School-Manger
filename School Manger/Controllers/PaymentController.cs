using Microsoft.AspNetCore.Mvc;
using School_Manager.Core.Services.Interfaces;
using School_Manger.PaymentService;

namespace School_Manger.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IPayBillService payBillService;
        private readonly IPayment payment;
        private readonly IBillService billService;
        public PaymentController(IPayment _payment, IPayBillService _payBillService, IBillService _billService)
        {
            payment = _payment;
            payBillService = _payBillService;
            billService = _billService;
        }
        public IActionResult Index(string Authority, string Status)
        {
            PayData data = payment.Get(Authority);
            if (data != null || Status == "OK")
            {
                payment.Clear(Authority);
                if (data == null)
                    data = new PayData() { Autratory = Authority };
                else
                {
                    var TotalPrice = data.BillIds.Select(x=>billService.GetBill(x).TotalPrice - billService.GetBill(x).PaidPrice).Sum();
                    payBillService.CreatePay(new School_Manager.Core.ViewModels.FModels.PayCreateDto()
                    {
                        BecomingTime = DateTime.Now,
                        PayType = School_Manager.Domain.Entities.Catalog.Enums.PayType.Internet,
                        Bills = data.BillIds,
                        Price = TotalPrice,
                        TrackingCode = Authority.Replace("0000",""),
                    });
                }
                return View("Index", data);
            }
            return Error(Authority);
        }
        public IActionResult Error(string Authority)
        {
            return View("Error", Authority);
        }
    }
}
