using Microsoft.AspNetCore.Mvc;
using School_Manager.Core.Services.Interfaces;
using School_Manager.Domain.Entities.Catalog.Operation;
using School_Manger.PaymentService;
using System.Threading.Tasks;

namespace School_Manger.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IPayBillService payBillService;
        private readonly IPayment payment;
        private readonly IBillService billService;
        private readonly IZarinPalService zarinPalService;
        public PaymentController(IPayment _payment, IPayBillService _payBillService, IBillService _billService, IZarinPalService zarinPalService)
        {
            payment = _payment;
            payBillService = _payBillService;
            billService = _billService;
            this.zarinPalService = zarinPalService;
        }
        public IActionResult Index(string Authority, string Status)
        {
            // Validate inputs
            if (string.IsNullOrEmpty(Authority) || Status != "OK")
                return Error(Authority);

            // Retrieve stored payment data
            var data = payment.Get(Authority);
            if (data == null)
                return Error(Authority);

            // Calculate total unpaid price
            var totalPrice = data.BillIds
                .Select(x => billService.GetBill(x).TotalPrice - billService.GetBill(x).PaidPrice)
                .Sum();
            Thread.Sleep(2000);
            // Verify payment with ZarinPal
            int resultCode = VerfiyPay(int.Parse(totalPrice.ToString()), Authority);
            if(resultCode == 0)
                return Error(Authority);


            if (resultCode == 100 || resultCode == 101)
            {
                // Clear temporary payment cache
                payment.Clear(Authority);
                foreach (var billId in data.BillIds)
                {
                    bool alreadyPaid = payBillService
                        .GetAllPays(billId)
                        .Any(x => x.TrackingCode == Authority.Replace("0000", ""));

                    if (!alreadyPaid)
                    {
                        payBillService.CreatePay(new School_Manager.Core.ViewModels.FModels.PayCreateDto()
                        {
                            BecomingTime = DateTime.Now,
                            PayType = School_Manager.Domain.Entities.Catalog.Enums.PayType.Internet,
                            Bills = data.BillIds,
                            Price = totalPrice,
                            TrackingCode = Authority.Replace("0000", "")
                        });
                    }
                }

                return View("Index", data);
            }
            else
            {
                return Error(Authority);
            }
        }
        public IActionResult Error(string Authority)
        {
            return View("Error", Authority);
        }
        private int VerfiyPay(int Amount, string Authority)
        {
            return zarinPalService.VerfiyPaymentAsync(Amount, Authority).GetAwaiter().GetResult();
        }
    }
}
