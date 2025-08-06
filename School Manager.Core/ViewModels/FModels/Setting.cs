using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Manager.Core.ViewModels.FModels
{
    public class SettingDto
    {
        public string Key { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public IFormFile File { get; set; }
    }
}
