using Dto.Payment;
using KingZarinPal;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Ocsp;
using School_Manager.Core.Services.Interfaces;
using Service;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ZarinPal;
using ZarinPal.Class;
namespace School_Manger.PaymentService
{
    public interface IZarinPalService
    {
        Task<string> RequestPaymentAsync(int amount, string description, string CallbackUrl, string PayEmail, string mobile = null);
        Task<string> TestRequestPaymentAsync(int amount, string description, string CallbackUrl, string PayEmail, string mobile = null);
        Task<int> VerfiyPaymentAsync(int amount, string Authority);
        Task<int> TestVerfiyPaymentAsync(int amount, string Authority);
        Task<int> TransactionInquiry(string authority);
        Task<int> TestTransactionInquiry(string authority);
    }

    public class ZarinPalService : IZarinPalService
    {

        private Payment _payment;
        private Authority _authority;
        private Transactions _transactions;
        private readonly string _merchantId;

        private readonly string Url = "https://www.zarinpal.com/";
        private readonly string TestUrl = "https://sandbox.zarinpal.com/";

        public ZarinPalService(string merchantId)
        {
            _merchantId = merchantId;
            var expose = new Expose();
            _payment = expose.CreatePayment();
            _authority = expose.CreateAuthority();
            _transactions = expose.CreateTransactions();
        }

        #region PaymentRequst

        public async Task<string> RequestPaymentAsync(int amount, string description, string CallbackUrl, string PayEmail, string mobile = null)
        {
            var result = await _payment.Request(new Dto.Payment.DtoRequest()
            {
                Mobile = null,//string.IsNullOrEmpty(mobile.Trim()) ? null : mobile,
                CallbackUrl = CallbackUrl,
                Description = description,
                Email = null,
                Amount = ((amount) + ((amount * 5) / 1000)), //Amount is Toman  Amount + 0.5% for Fee
                MerchantId = _merchantId,
            }, ZarinPal.Class.Payment.Mode.zarinpal);
            return $"{result.Authority}";
        }
        public async Task<string> TestRequestPaymentAsync(int amount, string description, string CallbackUrl, string PayEmail, string mobile = null)
        {
            if (string.IsNullOrEmpty(mobile))
                mobile = "09300000001";
            var result = new KingZarinPal.DtoRequest()
            {
                CallbackUrl = CallbackUrl,
                Description = description,
                Amount = ((amount) + ((amount * 5) / 1000)), //Amount is Toman  Amount + 0.5% for Fee
                MerchantId = _merchantId,
                MetaData = new MetaData()
                {
                    Email = PayEmail,
                    Mobile = string.IsNullOrEmpty(mobile.Trim()) ? "" : mobile,
                }
            };
            KingZarinPal.SendData<KingZarinPal.DtoRequest, PymentRequestToken> sendData =
                new KingZarinPal.SendData<KingZarinPal.DtoRequest, PymentRequestToken>(TestUrl + "pg/v4/payment/request.json", result);
            var response = await sendData.Post();
            return $"{response.Token.Authority}";
        }

        #endregion

        #region VerfiyPyment

        public async Task<int> VerfiyPaymentAsync(int amount, string Authority)
        {
            int StatusCode = 0;
            try
            {
                int TotalAmount = ((amount) + ((amount * 5) / 1000)) / 10; //Snyc it With Rial
                KingZarinPal.SendData<VerfiyApiRequest, VerfiyApiResponse> sendData =
                    new KingZarinPal.SendData<VerfiyApiRequest, VerfiyApiResponse>(Url + "pg/v4/payment/verify.json"
                    , new VerfiyApiRequest(_merchantId, Authority, TotalAmount));
                var response = await sendData.Post();
                StatusCode = response.Data.Code;
            }
            catch
            {

            }
            return StatusCode;
        }
        public async Task<int> TestVerfiyPaymentAsync(int amount, string Authority)
        {
            int StatusCode = 0;
            try
            {
                int TotalAmount = ((amount) + ((amount * 5) / 1000)) / 10; //Snyc it With Rial
                KingZarinPal.SendData<VerfiyApiRequest, VerfiyApiResponse> sendData =
                    new KingZarinPal.SendData<VerfiyApiRequest, VerfiyApiResponse>(TestUrl + "pg/v4/payment/verify.json"
                    , new VerfiyApiRequest(_merchantId, Authority, TotalAmount));
                var response = await sendData.Post();
                StatusCode = response.Data.Code;
            }
            catch
            {

            }
            return StatusCode;
        }

        #endregion

        #region Transaction Inquiry
        public async Task<int> TransactionInquiry(string authority)
        {
            KingZarinPal.SendData<InquiryRequst, InquiryResponse> sendData =
                    new KingZarinPal.SendData<InquiryRequst, InquiryResponse>(Url + "pg/v4/payment/inquiry.json"
                    , new InquiryRequst(_merchantId, authority));
            var response = await sendData.Post();
            return response.Code;
        }
        public async Task<int> TestTransactionInquiry(string authority)
        {
            KingZarinPal.SendData<InquiryRequst, InquiryResponse> sendData =
                    new KingZarinPal.SendData<InquiryRequst, InquiryResponse>(TestUrl + "pg/v4/payment/inquiry.json"
                    , new InquiryRequst(_merchantId, authority));
            var response = await sendData.Post();
            return response.Code;
        }
        #endregion
    }
}
