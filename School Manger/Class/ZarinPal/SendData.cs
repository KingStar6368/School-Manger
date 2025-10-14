using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace KingZarinPal
{

    public class SendData<TIn, TOut>
    {
        private readonly string _url;

        private readonly TIn _data;

        private TOut _outPut;

        public SendData(string url, TIn data)
        {
            _url = url;
            _data = data;
        }

        public async Task<TOut> Post()
        {
            string convertedData = JsonConvert.SerializeObject(_data);
            using (HttpClient httpClient = new HttpClient())
            {
                using HttpResponseMessage response = await httpClient.PostAsync(_url, new StringContent(convertedData, Encoding.UTF8, "application/json"));
                _outPut = JsonConvert.DeserializeObject<TOut>(await response.Content.ReadAsStringAsync());
            }

            return _outPut;
        }
    }
}