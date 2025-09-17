using SMS.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.TempLinkService
{
    public class TempLinkService : ITempLink
    {
        private readonly IAppConfigService appConfigService;
        public TempLinkService(IAppConfigService appConfig)
        {
            appConfigService = appConfig;
        }
        public string GenerateBillTempLink(long ParentID, long ChildID)
        {
            byte[] Bytedata = Encoding.UTF8.GetBytes($"{ParentID},{ChildID},{DateTime.Now}");
            string Data = "";
            foreach (byte b in Bytedata)
                Data += $"{b},";
            Data = Data.Substring(0, Data.Length - 1);
            string Link = $"{appConfigService.WebAddress()}/DirectLink/DirectBill?CodeData={Data}";
            return Link;

        }

        public string GenerateDriverDirectLink(long DriverId)
        {
            return "";
        }
    }
}
