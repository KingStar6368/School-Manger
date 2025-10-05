using Dto.Payment;
using Newtonsoft.Json;
using School_Manager.Core.Services.Interfaces;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ZarinPal;
using ZarinPal.Class;
namespace School_Manger.PaymentService
{
    public interface IZarinPalService
    {
        Task<string> RequestPaymentAsync(int amount, string description,string CallbackUrl, string PayEmail, string mobile = null);
    }

    public class ZarinPalService : IZarinPalService
    {

        private Payment _payment;
        private Authority _authority;
        private Transactions _transactions;
        private readonly string _merchantId;
        public ZarinPalService(string merchantId)
        {
            _merchantId = merchantId;
        }

        public async Task<string> RequestPaymentAsync(int amount, string description,string CallbackUrl,string PayEmail, string mobile = null)
        {
            var expose = new Expose();
            _payment = expose.CreatePayment();
            _authority = expose.CreateAuthority();
            _transactions = expose.CreateTransactions();
            var result = await _payment.Request(new DtoRequest()
            {
                Mobile = string.IsNullOrEmpty(mobile.Trim())?null:mobile,
                CallbackUrl = CallbackUrl,
                Description = description,
                Email = PayEmail,
                Amount = amount,
                MerchantId = _merchantId,
            }, ZarinPal.Class.Payment.Mode.zarinpal);
            return $"https://zarinpal.com/pg/StartPay/{result.Authority}";
        }
    }

    public class PaymentRequestResponse
    {
        public int code { get; set; }
        public string message { get; set; }
        public string authority { get; set; }
        public string fee_type { get; set; }
        public int fee { get; set; }
    }
}
