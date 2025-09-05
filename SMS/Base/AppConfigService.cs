using Microsoft.Extensions.Configuration;

namespace SMS.Base
{
    public interface IAppConfigService
    {
        string ConnectionString();
        string SMSKey();
        long SMSLine();
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

        public long SMSLine()
        {
            return long.Parse(_configuration["Sms:Line"]);
        }

        public string ApiUrl()
        {
            return _configuration["WebSetting:ApiUrl"];
        }

    }
}