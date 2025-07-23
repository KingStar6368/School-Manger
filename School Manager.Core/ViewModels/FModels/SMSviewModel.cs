using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Manager.Core.ViewModels.FModels
{
    public class SMSTempleDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long TempleId { get; set; }
    }
    public class SMSTempleUpdateDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long TempleId { get; set; }
    }
    public class SMSTempleDeleteDto
    {
        public long Id { get; set; }
    }
}
