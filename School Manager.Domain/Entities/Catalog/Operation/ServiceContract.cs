using School_Manager.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Manager.Domain.Entities.Catalog.Operation
{
    public class ServiceContract : AuditableEntity<long>
    {
        /// <summary>
        /// شناسه فرزند
        /// </summary>
        public long ChildRef { get; set; }
        /// <summary>
        /// مبلغ کل قرار داد
        /// </summary>
        public long TotalPrice { get; set; }
        /// <summary>
        /// مبلغ در هر ماه
        /// </summary>
        public long MonthPrice { get; set; }
        /// <summary>
        /// تاریخ شروع قرار داد
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// تاریخ اتمام قرار داد
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// عکس امضا
        /// </summary>
        public byte[] SignatureImage { get; set; }
        /// <summary>
        /// کلاس فرزند
        /// </summary>
        public virtual Child ChildNavigation { get; set; }
        /// <summary>
        /// کلاس چک
        /// </summary>
        public virtual ICollection<ServiceContractCheque> ServiceContractCheques { get; set; }
        public virtual ICollection<Bill> BillNavigation { get; set; }
    }
}
