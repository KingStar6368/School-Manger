using School_Manager.Core.ViewModels.FModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Manager.Core.Services.Interfaces
{
    public interface ISettingService
    {
        List<SettingDto> GetAllSetting();
        string Get(string key);
        byte[] GetImage(string key);
        bool SaveSetting(SettingDto dto);
        Task<bool> SaveSettingImage(SettingDto dto);
    }
}
