using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Manager.Core.ViewModels.FModels
{
    public class DebtorDto
    {
        public long ParentId { get; set; }
        public string ParentName { get; set; }
        public string ParentFamily { get; set; }
        public int ChildCount { get; set; }
        public long Price { get; set; }
        public DateTime? LastSentSMS { get; set; }
        public int SmsCount { set; get; }
    }
}
