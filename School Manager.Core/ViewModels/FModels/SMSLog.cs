using School_Manager.Domain.Entities.Catalog.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Manager.Core.ViewModels.FModels
{
    public class SMSLogDto
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public SMSType type {get;set;}
        public DateTime SMSTime { get; set; }
    }
}
