using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Manager.Core.Services.Implemetations
{
    public class SMSTempleDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public long TempleId { get; set; }
    }
    public class SMSTempleUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public long TempleId { get; set; }
    }
    public class SMSTempleDeleteDto
    {
        public int Id { get; set; }
    }
}
