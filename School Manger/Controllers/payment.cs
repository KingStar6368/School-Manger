#region Assembly ZarinpalSandbox, Version=2.0.4.0, Culture=neutral, PublicKeyToken=null
// C:\Users\mggg6\.nuget\packages\zarinpalsandbox\2.0.4\lib\netcoreapp1.0\ZarinpalSandbox.dll
// Decompiled with ICSharpCode.Decompiler 8.2.0.7535
#endregion

using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace King
{
    public class Payment
    {
        private const string MerchantId = "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx";
        private readonly int _amount;

        public Payment(int amount)
        {
            _amount = amount;
        }

        public async Task<PaymentRequestResponse> PaymentRequest(string description, string callbackUrl, string mobile = null)
        {
            PaymentRequestResponse result;
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("accept", "application/json");

                var requestData = new
                {
                    merchant_id = MerchantId,
                    amount = _amount,
                    description = description,
                    callback_url = callbackUrl,
                    mobile = mobile
                };

                string content = JsonConvert.SerializeObject(requestData);
                using HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(
                    "https://sandbox.zarinpal.com/pg/v4/payment/request.json",
                    new StringContent(content, Encoding.UTF8, "application/json"));

                string responseContent = await httpResponseMessage.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<Respance>(responseContent).data;
            }

            return result;
        }
    }
    public class Respance
    {
        public PaymentRequestResponse data;
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
#if false // Decompilation log
'370' items in cache
------------------
Resolve: 'System.Runtime, Version=4.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Runtime, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
WARN: Version mismatch. Expected: '4.1.0.0', Got: '9.0.0.0'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\9.0.7\ref\net9.0\System.Runtime.dll'
------------------
Resolve: 'System.Diagnostics.Debug, Version=4.0.10.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Diagnostics.Debug, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
WARN: Version mismatch. Expected: '4.0.10.0', Got: '9.0.0.0'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\9.0.7\ref\net9.0\System.Diagnostics.Debug.dll'
------------------
Resolve: 'System.Threading.Tasks, Version=4.0.10.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Threading.Tasks, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
WARN: Version mismatch. Expected: '4.0.10.0', Got: '9.0.0.0'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\9.0.7\ref\net9.0\System.Threading.Tasks.dll'
------------------
Resolve: 'System.Net.Http, Version=4.1.1.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Net.Http, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
WARN: Version mismatch. Expected: '4.1.1.0', Got: '9.0.0.0'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\9.0.7\ref\net9.0\System.Net.Http.dll'
------------------
Resolve: 'Newtonsoft.Json, Version=10.0.0.0, Culture=neutral, PublicKeyToken=30ad4fe6b2a6aeed'
Found single assembly: 'Newtonsoft.Json, Version=13.0.0.0, Culture=neutral, PublicKeyToken=30ad4fe6b2a6aeed'
WARN: Version mismatch. Expected: '10.0.0.0', Got: '13.0.0.0'
Load from: 'C:\Users\mggg6\.nuget\packages\newtonsoft.json\13.0.3\lib\net6.0\Newtonsoft.Json.dll'
------------------
Resolve: 'System.Collections, Version=4.0.10.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Collections, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
WARN: Version mismatch. Expected: '4.0.10.0', Got: '9.0.0.0'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\9.0.7\ref\net9.0\System.Collections.dll'
------------------
Resolve: 'System.Text.Encoding, Version=4.0.10.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Text.Encoding, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
WARN: Version mismatch. Expected: '4.0.10.0', Got: '9.0.0.0'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\9.0.7\ref\net9.0\System.Text.Encoding.dll'
------------------
Resolve: 'System.Runtime, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Runtime, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\9.0.7\ref\net9.0\System.Runtime.dll'
------------------
Resolve: 'System.Runtime.InteropServices, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'
Found single assembly: 'System.Runtime.InteropServices, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
WARN: Version mismatch. Expected: '1.0.0.0', Got: '9.0.0.0'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\9.0.7\ref\net9.0\System.Runtime.InteropServices.dll'
------------------
Resolve: 'System.Runtime.CompilerServices.Unsafe, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'
Found single assembly: 'System.Runtime.CompilerServices.Unsafe, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
WARN: Version mismatch. Expected: '1.0.0.0', Got: '9.0.0.0'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\9.0.7\ref\net9.0\System.Runtime.CompilerServices.Unsafe.dll'
#endif

