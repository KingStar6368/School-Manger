using School_Manager.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Manager.Domain.Entities.Catalog.Operation
{
    public class Bill : AuditableEntity<long>
    {
        /// <summary>
        /// کد قرار داد
        /// </summary>
        public long ServiceContractRef { get; set; }
        /// <summary>
        /// مبلغ کل قبض
        /// </summary>
        public long Price { get; set; }
        /// <summary>
        /// مهلت پرداخت
        /// </summary>
        public DateTime EstimateTime { get; set; }
        /// <summary>
        /// قرارداد
        /// </summary>
        public virtual ServiceContract ContractNavigation { get; set; }
        /// <summary>
        /// رابط قبض و پرداختی
        /// </summary>
        public virtual ICollection<PayBill> PayBills { get; set; }

    }
}
