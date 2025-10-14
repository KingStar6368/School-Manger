using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace KingZarinPal
{
    #region ZarinPalClass
    public class PymentRequestToken 
    {
        [JsonProperty("data")]
        public RequestToken Token { get; set; }

        [JsonProperty("errors")]
        public object Errors { get; set; }
    }
    public class RequestToken
    {
        [JsonProperty("code")]
        public int Status { get; set; }

        [JsonProperty("authority")]
        public string Authority { get; set; }

        [JsonProperty("fee")]
        public int Fee { get; set; }
        
        [JsonProperty("fee_type")]
        public string FeeType { get; set; }

    }
    public class DtoRequest
    {
        [JsonProperty("merchant_id")]
        public string MerchantId { get; set; }

        [JsonProperty("amount")]
        public int Amount { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("metadata")]
        public MetaData MetaData { get; set; }

        [JsonProperty("callback_url")]
        public string CallbackUrl { get; set; }
    }
    public class MetaData
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("mobile")]
        public string Mobile { get; set; }
    }
    public class VerfiyApiRequest
    {
        public VerfiyApiRequest(string MerchantID, string Authority, int Amount)
        {
            this.MerchantID = MerchantID;
            this.Authority = Authority;
            this.Amount = Amount;
        }
        [JsonProperty("merchant_id")]
        public string MerchantID { get; set; }
        [JsonProperty("authority")]
        public string Authority { get; set; }
        [JsonProperty("amount")]
        public int Amount { get; set; }
    }
    public class VerfiyApiResponse
    {
        [JsonPropertyName("data")]
        public VerfiyResponseData Data { get; set; }
    }
    public class VerfiyResponseData
    {
        [JsonPropertyName("code")]
        public int Code { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("card_hash")]
        public string CardHash { get; set; }

        [JsonPropertyName("card_pan")]
        public string CardPan { get; set; }

        [JsonPropertyName("ref_id")]
        public long RefId { get; set; }

        [JsonPropertyName("fee_type")]
        public string FeeType { get; set; }

        [JsonPropertyName("fee")]
        public decimal Fee { get; set; }

        [JsonPropertyName("shaparak_fee")]
        public decimal ShaparakFee { get; set; }

        [JsonPropertyName("order_id")]
        public string OrderId { get; set; }
    }
    public class VerfiyErrorResponse
    {
        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("code")]
        public int Code { get; set; }
    }
    public class PaymentRequestResponse
    {
        public int code { get; set; }
        public string message { get; set; }
        public string authority { get; set; }
        public string fee_type { get; set; }
        public int fee { get; set; }
    }
    #endregion
}
