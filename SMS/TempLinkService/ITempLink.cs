using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.TempLinkService
{
    public interface ITempLink
    {
        /// <summary>
        /// ساخت لینک موقت برای 2 روز با ورود اتومات برای خانواده
        /// </summary>
        /// <param name="ParentID"></param>
        /// <param name="ChildID"></param>
        /// <returns>لینک</returns>
        public string GenerateBillTempLink(long ParentID, long ChildID);
        /// <summary>
        /// ساخت لینک مستقیم راننده برای ورود اتوماتیک
        /// </summary>
        /// <param name="DriverId"></param>
        /// <returns>لینک</returns>
        public string GenerateDriverDirectLink(long DriverId);
    }
}
