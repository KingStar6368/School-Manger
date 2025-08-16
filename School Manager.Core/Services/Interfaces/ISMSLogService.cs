using School_Manager.Core.ViewModels.FModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Manager.Core.Services.Interfaces
{
    public interface ISMSLogService
    {
        public Task<SMSLogDto> GetSMSLog();
        public SMSLogDto GetDto(int id);
        public int GetWarningCount(long UserId);
        public int GetBillCount(long UserId);
        public int GetInfoCount(long UserId);
        public bool CreateSMSLog(SMSLogDto smsLog);
    }
}
