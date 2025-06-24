using School_Manager.Domain.Common;
using School_Manager.Domain.Entities.Catalog.Enums;
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
        /// نام قبض
        /// </summary>
        public string Name {  get; set; }
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
        /// نوع قبض (پیش پرداخت یا عادی) است
        /// </summary>
        public BillType Type { get; set; }
        /// <summary>
        /// قرارداد
        /// </summary>
        public virtual ServiceContract ServiceContractNavigation { get; set; }
        /// <summary>
        /// رابط قبض و پرداختی
        /// </summary>
        public virtual ICollection<PayBill> PayBills { get; set; }

    }
}
