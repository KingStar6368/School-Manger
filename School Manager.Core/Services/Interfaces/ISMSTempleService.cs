using School_Manager.Core.Services.Implemetations;
using School_Manager.Core.ViewModels.FModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Manager.Core.Services.Interfaces
{
    public interface ISMSTempleService
    {
        public Task<List<SMSTempleDto>> GetTemples();
        public SMSTempleDto GetTemple(long Id);
        public SMSTempleDto GetTempleByName(string Name);
        public SMSTempleDto GetTempleByTempleID(long TempleId);
        public bool UpdateTemple(SMSTempleUpdateDto updateDto);
        public bool DeleteTemple(long Id);
    }
}
