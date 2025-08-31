using Microsoft.Extensions.Configuration;

namespace SMS.Base
{
    public interface IAppConfigService
    {
        string ConnectionString();
        string SMSKey();
        string ApiUrl();
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

        public string ApiUrl()
        {
            return _configuration["WebSetting:ApiUrl"];
        }
    }
}