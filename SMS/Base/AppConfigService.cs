using Microsoft.Extensions.Configuration;

namespace SMS.Base
{
    public interface IAppConfigService
    {
        string ConnectionString();
        string SMSKey();
        long SMSLine();
        string WebAddress();
        string ApiUrl();
        bool SMSOtp();
        bool IsDemo();
    }

    public class AppConfigService : IAppConfigService
    {
        private readonly IConfiguration _configuration;

        public AppConfigService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string ConnectionString()
        {
            return _configuration.GetConnectionString("DefaultConnection");
        }

        public string SMSKey()
        {
            return _configuration["Sms:ApiKey"];
        }

        public long SMSLine()
        {
            return long.Parse(_configuration["Sms:Line"]);
        }

        public string ApiUrl()
        {
            return _configuration["WebSetting:ApiUrl"];
        }

        public string WebAddress()
        {
            return _configuration["WebSetting:WebAddress"];
        }
        public bool SMSOtp()
        {
            return _configuration["WebSetting:SMSOTP"].ToLower() == "true"?true:false;
        }
        public bool IsDemo()
        {
            try
            {
                return _configuration["WebSetting:Demo"].ToLower() == "true" ? true : false;
            }
            catch
            {
                return false;
            }
        }
    }
}