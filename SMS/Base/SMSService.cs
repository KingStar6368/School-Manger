using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Base
{
    public interface ISMSService
    {
        public bool Send(string Phone,string Message);
        public bool SendCode(string Phone,string Code);
        public Task<bool> Send2All(string[] Phones,string Message);
        public Task<bool> Send2Grup(string[] Phones,string Message);
    }
}
